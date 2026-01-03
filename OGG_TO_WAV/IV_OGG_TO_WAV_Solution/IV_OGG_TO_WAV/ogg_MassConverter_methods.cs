	using System;
	using System.IO;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Concurrent;
	using System.Diagnostics;
	using NAudio.Vorbis;
	using NAudio.Wave;

namespace IV_OGG_TO_WAV
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Concurrent;
	using System.Diagnostics;
	using NAudio.Vorbis;
	using NAudio.Wave;

	public class MassOggToWavConverter
	{
		private readonly ConcurrentBag<ConversionResult> _results = new();
		private readonly SemaphoreSlim _semaphore;
		private readonly IProgress<ConversionProgress> _progress;
		private int _totalFiles;
		private int _processedFiles;
		private int _successCount;
		private int _failureCount;

		public class ConversionResult
		{
			public string SourceFile { get; set; }
			public string OutputFile { get; set; }
			public bool Success { get; set; }
			public string ErrorMessage { get; set; }
			public TimeSpan Duration { get; set; }
		}

		public class ConversionProgress
		{
			public int TotalFiles { get; set; }
			public int ProcessedFiles { get; set; }
			public int SuccessCount { get; set; }
			public int FailureCount { get; set; }
			public string CurrentFile { get; set; }
			public double PercentComplete => TotalFiles > 0 ? (ProcessedFiles * 100.0) / TotalFiles : 0;
		}

		public MassOggToWavConverter(int maxConcurrency = 0, IProgress<ConversionProgress> progress = null)
		{
			// Default to logical processor count for optimal I/O and CPU balance
			maxConcurrency = maxConcurrency <= 0 ? Environment.ProcessorCount : maxConcurrency;
			_semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
			_progress = progress;
		}

		public async Task<ConversionResult[]> ConvertDirectoryAsync(
			string sourceDirectory,
			bool preserveDirectoryStructure = true,
			string outputDirectory = null,
			CancellationToken cancellationToken = default)
		{
			if (!Directory.Exists(sourceDirectory))
				throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory}");

			// If no output directory specified, use source directory (side-by-side conversion)
			outputDirectory ??= sourceDirectory;

			// Discover all OGG files
			var oggFiles = Directory.EnumerateFiles(sourceDirectory, "*.ogg", SearchOption.AllDirectories)
								   .ToArray();

			if (oggFiles.Length == 0)
			{
				Console.WriteLine("No OGG files found in the specified directory.");
				return Array.Empty<ConversionResult>();
			}

			_totalFiles = oggFiles.Length;
			Console.WriteLine($"Found {_totalFiles} OGG files to convert.");

			// Create conversion tasks
			var tasks = oggFiles.Select(async oggFile =>
			{
				await _semaphore.WaitAsync(cancellationToken);
				try
				{
					return await ConvertFileAsync(
						oggFile,
						sourceDirectory,
						outputDirectory,
						preserveDirectoryStructure,
						cancellationToken);
				}
				finally
				{
					_semaphore.Release();
				}
			});

			// Execute all conversions
			var results = await Task.WhenAll(tasks);

			return results;
		}

		private async Task<ConversionResult> ConvertFileAsync(
			string sourceFile,
			string sourceRoot,
			string outputRoot,
			bool preserveStructure,
			CancellationToken cancellationToken)
		{
			var stopwatch = Stopwatch.StartNew();
			var result = new ConversionResult { SourceFile = sourceFile };

			try
			{
				// Determine output path
				string outputFile;
				if (preserveStructure)
				{
					// Maintain directory structure
					var relativePath = Path.GetRelativePath(sourceRoot, sourceFile);
					outputFile = Path.Combine(outputRoot, Path.ChangeExtension(relativePath, ".wav"));

					// Create subdirectory if needed
					var outputDir = Path.GetDirectoryName(outputFile);
					if (!Directory.Exists(outputDir))
						Directory.CreateDirectory(outputDir);
				}
				else
				{
					// Flat structure - all files in output root
					var fileName = Path.GetFileNameWithoutExtension(sourceFile) + ".wav";
					outputFile = Path.Combine(outputRoot, fileName);
				}

				result.OutputFile = outputFile;

				// Report progress
				ReportProgress(sourceFile);

				// Skip if target already exists and is newer than source
				if (File.Exists(outputFile))
				{
					var sourceInfo = new FileInfo(sourceFile);
					var targetInfo = new FileInfo(outputFile);

					if (targetInfo.LastWriteTime > sourceInfo.LastWriteTime && targetInfo.Length > 0)
					{
						Console.WriteLine($"Skipping (already converted): {Path.GetFileName(sourceFile)}");
						result.Success = true;
						Interlocked.Increment(ref _successCount);
						return result;
					}
				}

				// Perform conversion
				await Task.Run(() =>
				{
					using (var vorbisReader = new VorbisWaveReader(sourceFile))
					{
						// For large files, use buffered approach
						var sourceInfo = new FileInfo(sourceFile);
						if (sourceInfo.Length > 50_000_000) // > 50MB
						{
							ConvertLargeFile(vorbisReader, outputFile, cancellationToken);
						}
						else
						{
							// Small files can be converted directly
							WaveFileWriter.CreateWaveFile(outputFile, vorbisReader);
						}
					}
				}, cancellationToken);

				result.Success = true;
				Interlocked.Increment(ref _successCount);

				Console.WriteLine($"✓ Converted: {Path.GetFileName(sourceFile)} → {Path.GetFileName(outputFile)} " +
								$"({stopwatch.ElapsedMilliseconds}ms)");
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.ErrorMessage = ex.Message;
				Interlocked.Increment(ref _failureCount);

				Console.WriteLine($"✗ Failed: {Path.GetFileName(sourceFile)} - {ex.Message}");
			}
			finally
			{
				stopwatch.Stop();
				result.Duration = stopwatch.Elapsed;
				Interlocked.Increment(ref _processedFiles);
				_results.Add(result);
			}

			return result;
		}

		private void ConvertLargeFile(
			VorbisWaveReader reader,
			string outputPath,
			CancellationToken cancellationToken)
		{
			const int bufferSize = 1024 * 1024 * 4; // 4MB buffer for better I/O throughput

			using (var writer = new WaveFileWriter(outputPath, reader.WaveFormat))
			{
				var buffer = new byte[bufferSize];
				int bytesRead;

				while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
				{
					cancellationToken.ThrowIfCancellationRequested();
					writer.Write(buffer, 0, bytesRead);
				}
			}
		}

		private void ReportProgress(string currentFile)
		{
			_progress?.Report(new ConversionProgress
			{
				TotalFiles = _totalFiles,
				ProcessedFiles = _processedFiles,
				SuccessCount = _successCount,
				FailureCount = _failureCount,
				CurrentFile = Path.GetFileName(currentFile)
			});
		}

		public void PrintSummary(ConversionResult[] results)
		{
			Console.WriteLine("\n" + new string('=', 60));
			Console.WriteLine("CONVERSION SUMMARY");
			Console.WriteLine(new string('=', 60));
			Console.WriteLine($"Total files processed: {results.Length}");
			Console.WriteLine($"Successful conversions: {results.Count(r => r.Success)}");
			Console.WriteLine($"Failed conversions: {results.Count(r => !r.Success)}");
			Console.WriteLine($"Total time: {results.Sum(r => r.Duration.TotalSeconds):F2} seconds");

			if (results.Any(r => !r.Success))
			{
				Console.WriteLine("\nFailed files:");
				foreach (var failure in results.Where(r => !r.Success))
				{
					Console.WriteLine($"  - {failure.SourceFile}: {failure.ErrorMessage}");
				}
			}
		}
	}
}
