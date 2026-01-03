using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using NAudio.Vorbis;
using NAudio.Wave;

namespace IV_OGG_TO_WAV
{

	public class OggToWavConverter
	{
		private readonly ActionBlock<ConversionTask> _conversionPipeline;
		private readonly TransformBlock<string, ConversionTask> _discoveryBlock;

		public OggToWavConverter(int maxDegreeOfParallelism = -1)
		{
			// Discovery stage - finds files and creates tasks
			_discoveryBlock = new TransformBlock<string, ConversionTask>(
				path => new ConversionTask
				{
					InputPath = path,
					OutputPath = Path.ChangeExtension(path, ".wav")
				},
				new ExecutionDataflowBlockOptions
				{
					MaxDegreeOfParallelism = 1
				});

			// Conversion stage - actual OGG to WAV conversion
			_conversionPipeline = new ActionBlock<ConversionTask>(
				async task => await ConvertFileAsync(task),
				new ExecutionDataflowBlockOptions
				{
					MaxDegreeOfParallelism = maxDegreeOfParallelism == -1
						? Environment.ProcessorCount
						: maxDegreeOfParallelism,
					BoundedCapacity = Environment.ProcessorCount * 2
				});

			_discoveryBlock.LinkTo(_conversionPipeline,
				new DataflowLinkOptions { PropagateCompletion = true });
		}

		private async Task ConvertFileAsync(ConversionTask task)
		{
			await Task.Run(() =>
			{
				using (var vorbisReader = new VorbisWaveReader(task.InputPath))
				{
					// Use memory-mapped file for large files
					if (new FileInfo(task.InputPath).Length > 100_000_000)
					{
						ConvertLargeFile(vorbisReader, task.OutputPath);
					}
					else
					{
						WaveFileWriter.CreateWaveFile(task.OutputPath, vorbisReader);
					}
				}
			});
		}

		private void ConvertLargeFile(VorbisWaveReader reader, string outputPath)
		{
			using (var writer = new WaveFileWriter(outputPath, reader.WaveFormat))
			{
				var buffer = new byte[1024 * 1024]; // 1MB buffer
				int bytesRead;

				while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
				{
					writer.Write(buffer, 0, bytesRead);
				}
			}
		}

		public async Task ConvertDirectoryAsync(string inputDir, string outputDir,
			string searchPattern = "*.ogg")
		{
			var files = Directory.EnumerateFiles(inputDir, searchPattern,
				SearchOption.AllDirectories);

			foreach (var file in files)
			{
				var relativePath = Path.GetRelativePath(inputDir, file);
				var outputPath = Path.Combine(outputDir,
					Path.ChangeExtension(relativePath, ".wav"));

				Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

				await _discoveryBlock.SendAsync(file);
			}

			_discoveryBlock.Complete();
			await _conversionPipeline.Completion;
		}
	}

	public class ConversionTask
	{
		public string InputPath { get; set; }
		public string OutputPath { get; set; }
	}

}
