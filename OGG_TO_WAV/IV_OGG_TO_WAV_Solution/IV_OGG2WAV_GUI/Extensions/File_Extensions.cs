using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace IV_OGG2WAV_GUI.Extensions
{
		public static class FileEquality_Basic
	{
		/// <summary>
		/// Returns true only if both paths refer to files that match on:
		/// - normalized name (case-insensitive)
		/// - length (bytes)
		/// - full byte-for-byte content
		/// 
		/// Notes:
		/// - Uses fast prechecks then a streaming hash comparison.
		/// - Avoids reading entire files into memory.
		/// - Throws for invalid paths / access issues (caller can catch).
		/// </summary>
		public static bool AreIdenticalBasic(this string filePathA, string filePathB)
		{
			if (string.IsNullOrWhiteSpace(filePathA)) throw new ArgumentException("Path A is null/empty.", nameof(filePathA));
			if (string.IsNullOrWhiteSpace(filePathB)) throw new ArgumentException("Path B is null/empty.", nameof(filePathB));

			// Same string (and same full path) is trivially identical.
			if (string.Equals(filePathA, filePathB, StringComparison.Ordinal))
				return true;

			var fiA = new FileInfo(filePathA);
			var fiB = new FileInfo(filePathB);

			if (!fiA.Exists || !fiB.Exists)
				return false;

			// Name compare (case-insensitive for Windows; safe for cross-platform "same name" intent).
			// If you want strict behavior for Linux/macOS, change to Ordinal.
			if (!string.Equals(fiA.Name, fiB.Name, StringComparison.OrdinalIgnoreCase))
				return false;

			// Size compare
			if (fiA.Length != fiB.Length)
				return false;

			// If both are zero-length and same name, they are identical.
			if (fiA.Length == 0)
				return true;

			// Content compare via streaming hash (fast, low memory).
			// Using SHA-256 for strong collision resistance.
			byte[] hashA = ComputeSha256(filePathA);
			byte[] hashB = ComputeSha256(filePathB);

			return CryptographicOperations.FixedTimeEquals(hashA, hashB);
		}

		private static byte[] ComputeSha256(string path)
		{
			// FileStream settings tuned for sequential reads
			using var stream = new FileStream(
				path,
				FileMode.Open,
				FileAccess.Read,
				FileShare.Read,
				bufferSize: 1024 * 1024, // 1MB buffer
				options: FileOptions.SequentialScan);

			using var sha = SHA256.Create();
			return sha.ComputeHash(stream);
		}
	}


public static class FileEqualityFast
	{
		/// <summary>
		/// Windows-fast path using native file handles + memory mapping + memcmp.
		/// Checks:
		/// - exists
		/// - name (case-insensitive)
		/// - size
		/// - byte-for-byte content
		/// </summary>
		public static bool AreIdenticalFast(this string filePathA, string filePathB)
		{
			if (string.IsNullOrWhiteSpace(filePathA)) throw new ArgumentException("Path A is null/empty.", nameof(filePathA));
			if (string.IsNullOrWhiteSpace(filePathB)) throw new ArgumentException("Path B is null/empty.", nameof(filePathB));

			if (string.Equals(filePathA, filePathB, StringComparison.Ordinal))
				return true;

			var fiA = new FileInfo(filePathA);
			var fiB = new FileInfo(filePathB);

			if (!fiA.Exists || !fiB.Exists)
				return false;

			if (!string.Equals(fiA.Name, fiB.Name, StringComparison.OrdinalIgnoreCase))
				return false;

			if (fiA.Length != fiB.Length)
				return false;

			if (fiA.Length == 0)
				return true;

			// Windows native fast path
			if (OperatingSystem.IsWindows())
				return NativeByteForByteEqualWindows(filePathA, filePathB, fiA.Length);

			// Fallback (non-Windows): managed streaming compare
			return ManagedByteForByteEqual(filePathA, filePathB);
		}

		private static bool ManagedByteForByteEqual(string pathA, string pathB)
		{
			const int BufferSize = 1024 * 1024;

			using var fsA = new FileStream(pathA, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.SequentialScan);
			using var fsB = new FileStream(pathB, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.SequentialScan);

			byte[] a = GC.AllocateUninitializedArray<byte>(BufferSize);
			byte[] b = GC.AllocateUninitializedArray<byte>(BufferSize);

			while (true)
			{
				int ra = fsA.Read(a, 0, a.Length);
				int rb = fsB.Read(b, 0, b.Length);

				if (ra != rb) return false;
				if (ra == 0) return true;

				if (!a.AsSpan(0, ra).SequenceEqual(b.AsSpan(0, rb)))
					return false;
			}
		}

		private static unsafe bool NativeByteForByteEqualWindows(string pathA, string pathB, long length)
		{
			// Open handles (unbuffered is NOT used here; mapping is already efficient and OS-managed)
			using var hA = SafeCreateFile(pathA);
			using var hB = SafeCreateFile(pathB);

			if (hA.IsInvalid || hB.IsInvalid) return false;

			// If they are literally the same file on disk (same file ID), they are identical.
			if (TrySameFileId(hA, hB))
				return true;

			// Memory-map and compare in chunks to avoid huge single mappings.
			// 256MB chunks are a good default; adjust if needed.
			const long ChunkSize = 256L * 1024 * 1024;

			using var mapA = SafeCreateFileMapping(hA);
			using var mapB = SafeCreateFileMapping(hB);

			if (mapA.IsInvalid || mapB.IsInvalid) return false;

			long offset = 0;
			while (offset < length)
			{
				long remaining = length - offset;
				long viewSize = remaining < ChunkSize ? remaining : ChunkSize;

				using var viewA = SafeMapView(mapA, offset, viewSize);
				using var viewB = SafeMapView(mapB, offset, viewSize);

				if (viewA.Address == null || viewB.Address == null)
					return false;

				// Native memcmp is very fast and typically vectorized by the CRT.
				if (memcmp(viewA.Address, viewB.Address, (UIntPtr)viewSize) != 0)
					return false;

				offset += viewSize;
			}

			return true;
		}

		// -------------------- Native helpers (Windows) --------------------

		private static SafeFileHandleEx SafeCreateFile(string path)
		{
			// FILE_SHARE_READ lets other readers coexist; sequential scan hint can help caching.
			var h = CreateFileW(
				path,
				GENERIC_READ,
				FILE_SHARE_READ,
				IntPtr.Zero,
				OPEN_EXISTING,
				FILE_ATTRIBUTE_NORMAL | FILE_FLAG_SEQUENTIAL_SCAN,
				IntPtr.Zero);

			return new SafeFileHandleEx(h);
		}

		private static SafeMappingHandle SafeCreateFileMapping(SafeFileHandleEx file)
		{
			IntPtr hMap = CreateFileMappingW(file.DangerousGetHandle(), IntPtr.Zero, PAGE_READONLY, 0, 0, null);
			return new SafeMappingHandle(hMap);
		}

		private static unsafe MappedView SafeMapView(SafeMappingHandle map, long offset, long size)
		{
			uint offLow = unchecked((uint)(offset & 0xFFFFFFFF));
			uint offHigh = unchecked((uint)((offset >> 32) & 0xFFFFFFFF));

			void* addr = MapViewOfFile(map.DangerousGetHandle(), FILE_MAP_READ, offHigh, offLow, (UIntPtr)size);
			return new MappedView(addr, (UIntPtr)size);
		}

		private static bool TrySameFileId(SafeFileHandleEx a, SafeFileHandleEx b)
		{
			// Same volume serial + file index => same file object.
			if (!GetFileInformationByHandle(a.DangerousGetHandle(), out var ia)) return false;
			if (!GetFileInformationByHandle(b.DangerousGetHandle(), out var ib)) return false;

			return ia.dwVolumeSerialNumber == ib.dwVolumeSerialNumber &&
				   ia.nFileIndexHigh == ib.nFileIndexHigh &&
				   ia.nFileIndexLow == ib.nFileIndexLow;
		}

		// -------------------- P/Invoke --------------------

		private const uint GENERIC_READ = 0x80000000;
		private const uint FILE_SHARE_READ = 0x00000001;
		private const uint OPEN_EXISTING = 3;

		private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
		private const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;

		private const uint PAGE_READONLY = 0x02;
		private const uint FILE_MAP_READ = 0x0004;

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr CreateFileW(
			string lpFileName,
			uint dwDesiredAccess,
			uint dwShareMode,
			IntPtr lpSecurityAttributes,
			uint dwCreationDisposition,
			uint dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr CreateFileMappingW(
			IntPtr hFile,
			IntPtr lpFileMappingAttributes,
			uint flProtect,
			uint dwMaximumSizeHigh,
			uint dwMaximumSizeLow,
			string? lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern unsafe void* MapViewOfFile(
			IntPtr hFileMappingObject,
			uint dwDesiredAccess,
			uint dwFileOffsetHigh,
			uint dwFileOffsetLow,
			UIntPtr dwNumberOfBytesToMap);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetFileInformationByHandle(
			IntPtr hFile,
			out BY_HANDLE_FILE_INFORMATION lpFileInformation);

		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe int memcmp(void* b1, void* b2, UIntPtr count);

		[StructLayout(LayoutKind.Sequential)]
		private struct BY_HANDLE_FILE_INFORMATION
		{
			public uint dwFileAttributes;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
			public uint dwVolumeSerialNumber;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint nNumberOfLinks;
			public uint nFileIndexHigh;
			public uint nFileIndexLow;
		}

		// -------------------- Safe handles / view --------------------

		private sealed class SafeFileHandleEx : SafeHandle
		{
			public SafeFileHandleEx(IntPtr handle) : base(IntPtr.Zero, ownsHandle: true) => SetHandle(handle);
			public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);
			protected override bool ReleaseHandle() => CloseHandle(handle);
		}

		private sealed class SafeMappingHandle : SafeHandle
		{
			public SafeMappingHandle(IntPtr handle) : base(IntPtr.Zero, ownsHandle: true) => SetHandle(handle);
			public override bool IsInvalid => handle == IntPtr.Zero;
			protected override bool ReleaseHandle() => CloseHandle(handle);
		}

		private readonly unsafe struct MappedView : IDisposable
		{
			public readonly void* Address;
			public readonly UIntPtr Size;

			public MappedView(void* address, UIntPtr size)
			{
				Address = address;
				Size = size;
			}

			public void Dispose()
			{
				if (Address != null)
					UnmapViewOfFile((IntPtr)Address);
			}
		}
	}

}
