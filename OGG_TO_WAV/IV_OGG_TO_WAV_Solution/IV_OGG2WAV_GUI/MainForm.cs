using System.Diagnostics;
using System.Text;

namespace IV_OGG2WAV_GUI
{
	
	public partial class MainForm : Form
	{

		public static string EXE_PATH = "IV_OGG2WAV.exe";
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

		private void ExecuteProcessBtn_Click(object sender, EventArgs e)
		{

			// Example usage:
			string exePath = EXE_PATH;
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

		private void SelectDeselectAll_Click(object sender, EventArgs e)
		{
			if (SelectDeselectAll.Text == "Select All")
			{
				for (int i = 0;i < CheckedOGGListBox.Items.Count;i++)
				{
					CheckedOGGListBox.SetItemChecked(i, true);
				}
				SelectDeselectAll.Text = "Deselect All";
			}
			else
			{
				for (int i = 0;i < CheckedOGGListBox.Items.Count;i++)
				{
					CheckedOGGListBox.SetItemChecked(i, false);
				}
				SelectDeselectAll.Text = "Select All";
			}
		}

		private void RemoveDuplicates_Click(object sender, EventArgs e)
		{
			for (int o = 0;o < CheckedOGGListBox.Items.Count;o++)
			{
				string currentItem = CheckedOGGListBox.Items[o].ToString();
				for (int i = 0;i < CheckedOGGListBox.Items.Count;i++)
				{
					if (i != o && CheckedOGGListBox.Items[i].ToString() == currentItem)
					{
						// TODO ADD FILE SIZE CHECK
						CheckedOGGListBox.Items.RemoveAt(i);
						if (i < o)
						{
							o--;
						}
						i--;
					}
				}
			}
		}

		private void AddFilesSelectOGG_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = true,
				Filter = "OGG Files (*.ogg)|*.ogg|All Files (*.*)|*.*"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (string file in openFileDialog.FileNames)
				{
					// TODO STORE FILE NAME and Store Full Name in List
					CheckedOGGListBox.Items.Add(file, true);
				}
			}
			RemoveDuplicates_Click(sender, e);
		}

		private void AddOGGFilesRecursivly_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				string[] oggFiles = Directory.GetFiles(selectedPath, "*.ogg", SearchOption.AllDirectories);
				foreach (string file in oggFiles)
				{
					// TODO STORE FILE NAME and Store Full Name in List
					CheckedOGGListBox.Items.Add(file, true);
				}
			}
			RemoveDuplicates_Click(sender, e);
		}
	}
}
