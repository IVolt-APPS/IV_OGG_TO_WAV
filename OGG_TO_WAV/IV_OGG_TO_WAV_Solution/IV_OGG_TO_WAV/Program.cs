using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace IV_OGG_TO_WAV
{
	class Program
	{
		static void DrawHelp()
		{
			Console.WriteLine("Usage: OggToWav.exe <directory> [output_directory] [--flat]");
			Console.WriteLine("  <directory>: Source directory containing OGG files");
			Console.WriteLine("  [output_directory]: Optional. Defaults to source directory");
			Console.WriteLine("  [--flat]: Optional. Don't preserve directory structure");
			Console.WriteLine("  ------------------------------------------------------");
			Console.WriteLine("  Internal Console Quesion start Converting = 'convert' | 'c' | 'start'");
			Console.WriteLine("  Internal Console Quesion exit app = 'quit' | 'q' | 'exit'");
			return;
		}

		static async Task Main(string[] args)
		{
			var sourceDir = "";
			var outputDir = sourceDir;
			var preserveStructure = true;

			if (args.Count() > 0)
			{
				sourceDir = args[0];
				outputDir = args.Length > 1 && !args[1].StartsWith("--") && Directory.Exists(args[1]) ? args[1] : sourceDir;
				preserveStructure = !args.Any(a => a == "--flat");

			}
			else
			{
			startApp:
				Console.Clear();
				Console.WriteLine("help: Show Help | quit: shutdown | start: run app");
				Console.Write("Type Your Choice and Hit Enter: ");
				var _Action = Console.ReadLine();
				if (_Action != null && (_Action.Equals("help", StringComparison.OrdinalIgnoreCase) || _Action.Equals("-h", StringComparison.OrdinalIgnoreCase) || _Action.Equals("--help", StringComparison.OrdinalIgnoreCase)))
				{
					DrawHelp();
					return;
				}
				else if (_Action != null && (_Action.Equals("convert", StringComparison.OrdinalIgnoreCase) || _Action.Equals("c", StringComparison.OrdinalIgnoreCase) || _Action.Equals("start", StringComparison.OrdinalIgnoreCase)))
				{
				startconvertquestions:
					Console.Clear();
					Console.WriteLine("Please provide the source directory path:");
					var _SourceDir = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(_SourceDir) || Directory.Exists(_SourceDir) == false)
					{						
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Source directory cannot be empty and must exist.");
						Console.ReadKey();
						Console.ResetColor();
						goto startconvertquestions;
					}
					sourceDir = _SourceDir;
					outputDir = sourceDir;
				}
				else if (_Action != null && (_Action.Equals("quit", StringComparison.OrdinalIgnoreCase) || _Action.Equals("q", StringComparison.OrdinalIgnoreCase) || _Action.Equals("exit", StringComparison.OrdinalIgnoreCase)))
				{
					Environment.Exit(20);
					return;
				}
				else
				{
					Console.WriteLine("No arguments provided. Use 'help' for usage information.");
					goto startApp;
				}
			}


			// Set up progress reporting
			var progress = new Progress<MassOggToWavConverter.ConversionProgress>(p =>
			{
				// Update console title with progress
				Console.Title = $"Converting: {p.PercentComplete:F1}% ({p.ProcessedFiles}/{p.TotalFiles})";
			});

			var converter = new MassOggToWavConverter(
				maxConcurrency: Environment.ProcessorCount,
				progress: progress);

			var cts = new CancellationTokenSource();

			// Handle Ctrl+C gracefully
			Console.CancelKeyPress += (s, e) =>
			{
				e.Cancel = true;
				cts.Cancel();
				Console.WriteLine("\nCancellation requested. Waiting for current operations to complete...");
			};

			try
			{
				var stopwatch = Stopwatch.StartNew();

				var results = await converter.ConvertDirectoryAsync(
					sourceDir,
					preserveStructure,
					outputDir,
					cts.Token);

				stopwatch.Stop();

				converter.PrintSummary(results);
				Console.WriteLine($"\nTotal elapsed time: {stopwatch.Elapsed:hh\\:mm\\:ss}");
				Console.Write("Press Any Key To Exit"); Console.ReadKey();
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("Operation cancelled by user.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Fatal error: {ex.Message}");
				Environment.Exit(1);
			}
		}
	}
}
