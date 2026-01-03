namespace IV_OGG2WAV_GUI
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			AddOGGFilesRecursivly = new Button();
			CheckedOGGListBox = new CheckedListBox();
			AddFilesSelectOGG = new Button();
			SelectDeselectAll = new Button();
			RemoveDuplicates = new Button();
			OutputTextBox = new TextBox();
			SaveToMemoryButton = new Button();
			ExecuteProcessBtn = new Button();
			SuspendLayout();
			// 
			// AddOGGFilesRecursivly
			// 
			AddOGGFilesRecursivly.BackColor = Color.Black;
			AddOGGFilesRecursivly.FlatStyle = FlatStyle.Popup;
			AddOGGFilesRecursivly.Font = new Font("Consolas", 8.25F);
			AddOGGFilesRecursivly.ForeColor = Color.Lime;
			AddOGGFilesRecursivly.Location = new Point(12, 7);
			AddOGGFilesRecursivly.Name = "AddOGGFilesRecursivly";
			AddOGGFilesRecursivly.Size = new Size(156, 27);
			AddOGGFilesRecursivly.TabIndex = 0;
			AddOGGFilesRecursivly.Text = "Add Folder Recursivly";
			AddOGGFilesRecursivly.UseVisualStyleBackColor = false;
			// 
			// CheckedOGGListBox
			// 
			CheckedOGGListBox.FormattingEnabled = true;
			CheckedOGGListBox.Location = new Point(12, 73);
			CheckedOGGListBox.Name = "CheckedOGGListBox";
			CheckedOGGListBox.Size = new Size(355, 436);
			CheckedOGGListBox.TabIndex = 1;
			// 
			// AddFilesSelectOGG
			// 
			AddFilesSelectOGG.BackColor = Color.Black;
			AddFilesSelectOGG.FlatStyle = FlatStyle.Popup;
			AddFilesSelectOGG.Font = new Font("Consolas", 8.25F);
			AddFilesSelectOGG.ForeColor = Color.Lime;
			AddFilesSelectOGG.Location = new Point(12, 40);
			AddFilesSelectOGG.Name = "AddFilesSelectOGG";
			AddFilesSelectOGG.Size = new Size(156, 27);
			AddFilesSelectOGG.TabIndex = 2;
			AddFilesSelectOGG.Text = "Add OGG Files";
			AddFilesSelectOGG.UseVisualStyleBackColor = false;
			// 
			// SelectDeselectAll
			// 
			SelectDeselectAll.BackColor = Color.Black;
			SelectDeselectAll.FlatStyle = FlatStyle.Popup;
			SelectDeselectAll.Font = new Font("Consolas", 8.25F);
			SelectDeselectAll.ForeColor = Color.Lime;
			SelectDeselectAll.Location = new Point(202, 40);
			SelectDeselectAll.Name = "SelectDeselectAll";
			SelectDeselectAll.Size = new Size(165, 27);
			SelectDeselectAll.TabIndex = 3;
			SelectDeselectAll.Text = "Select/DeSelect All Files";
			SelectDeselectAll.UseVisualStyleBackColor = false;
			// 
			// RemoveDuplicates
			// 
			RemoveDuplicates.BackColor = Color.Black;
			RemoveDuplicates.FlatStyle = FlatStyle.Popup;
			RemoveDuplicates.Font = new Font("Consolas", 8.25F);
			RemoveDuplicates.ForeColor = Color.Lime;
			RemoveDuplicates.Location = new Point(202, 7);
			RemoveDuplicates.Name = "RemoveDuplicates";
			RemoveDuplicates.Size = new Size(165, 27);
			RemoveDuplicates.TabIndex = 4;
			RemoveDuplicates.Text = "Check And Remove Dups";
			RemoveDuplicates.UseVisualStyleBackColor = false;
			// 
			// OutputTextBox
			// 
			OutputTextBox.Location = new Point(373, 73);
			OutputTextBox.Multiline = true;
			OutputTextBox.Name = "OutputTextBox";
			OutputTextBox.Size = new Size(415, 436);
			OutputTextBox.TabIndex = 6;
			// 
			// SaveToMemoryButton
			// 
			SaveToMemoryButton.BackColor = Color.Black;
			SaveToMemoryButton.BackgroundImageLayout = ImageLayout.None;
			SaveToMemoryButton.FlatAppearance.BorderColor = Color.Black;
			SaveToMemoryButton.FlatAppearance.BorderSize = 0;
			SaveToMemoryButton.FlatStyle = FlatStyle.Popup;
			SaveToMemoryButton.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			SaveToMemoryButton.ForeColor = Color.White;
			SaveToMemoryButton.Location = new Point(655, 29);
			SaveToMemoryButton.Name = "SaveToMemoryButton";
			SaveToMemoryButton.Size = new Size(133, 38);
			SaveToMemoryButton.TabIndex = 7;
			SaveToMemoryButton.Text = "Save Results";
			SaveToMemoryButton.UseVisualStyleBackColor = false;
			// 
			// ExecuteProcessBtn
			// 
			ExecuteProcessBtn.BackColor = Color.Black;
			ExecuteProcessBtn.BackgroundImageLayout = ImageLayout.None;
			ExecuteProcessBtn.FlatAppearance.BorderColor = Color.Black;
			ExecuteProcessBtn.FlatAppearance.BorderSize = 0;
			ExecuteProcessBtn.FlatStyle = FlatStyle.Popup;
			ExecuteProcessBtn.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ExecuteProcessBtn.ForeColor = Color.White;
			ExecuteProcessBtn.Location = new Point(373, 7);
			ExecuteProcessBtn.Name = "ExecuteProcessBtn";
			ExecuteProcessBtn.Size = new Size(133, 61);
			ExecuteProcessBtn.TabIndex = 8;
			ExecuteProcessBtn.Text = "Execute Process";
			ExecuteProcessBtn.UseVisualStyleBackColor = false;
			ExecuteProcessBtn.Click += ExecuteProcessBtn_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Gray;
			ClientSize = new Size(793, 512);
			Controls.Add(ExecuteProcessBtn);
			Controls.Add(SaveToMemoryButton);
			Controls.Add(OutputTextBox);
			Controls.Add(RemoveDuplicates);
			Controls.Add(SelectDeselectAll);
			Controls.Add(AddFilesSelectOGG);
			Controls.Add(CheckedOGGListBox);
			Controls.Add(AddOGGFilesRecursivly);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MainForm";
			Text = "IV OGG to WAV GUI";
			Load += MainForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button AddOGGFilesRecursivly;
		private CheckedListBox CheckedOGGListBox;
		private Button AddFilesSelectOGG;
		private Button SelectDeselectAll;
		private Button RemoveDuplicates;
		private TextBox OutputTextBox;
		private Button SaveToMemoryButton;
		private Button ExecuteProcessBtn;
	}
}
