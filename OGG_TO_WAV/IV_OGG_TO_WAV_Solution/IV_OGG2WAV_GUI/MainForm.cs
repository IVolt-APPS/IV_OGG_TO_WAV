using System.Diagnostics;
using System.Text;

namespace IV_OGG2WAV_GUI
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

		private void ExecuteProcessBtn_Click(object sender, EventArgs e)
		{
			private void ExecuteProcessBtn_Click(object sender, EventArgs e)
		{
			// Example usage:
			string exePath = "your_executable.exe";
			string parameters = "--help";
			string output = RunProcessWithOutput(exePath, parameters);
			MessageBox.Show(output, "Process Output");
		}

		/// <summary>
		/// Runs an executable with parameters and returns all output strings (stdout + stderr).
		/// </summary>
		/// <param name="exePath">Path to the executable.</param>
		/// <param name="arguments">Command-line arguments.</param>
		/// <returns>Combined output from standard output and error.</returns>
		private string RunProcessWithOutput(string exePath, string arguments)
		{
			var outputBuilder = new StringBuilder();

			using (var process = new Process())
			{
				process.StartInfo.FileName = exePath;
				process.StartInfo.Arguments = arguments;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;

				process.OutputDataReceived += (sender, e) =>
				{
					if (e.Data != null)
						outputBuilder.AppendLine(e.Data);
				};
				process.ErrorDataReceived += (sender, e) =>
				{
					if (e.Data != null)
						outputBuilder.AppendLine(e.Data);
				};

				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
			}

			return outputBuilder.ToString();
		}
	}	
	}
}
