namespace IV_OGG2WAV_GUI
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) components.Dispose();
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

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
			RootLayout = new TableLayoutPanel();
			TopBar = new FlowLayoutPanel();
			ContentLayout = new TableLayoutPanel();
			RootLayout.SuspendLayout();
			TopBar.SuspendLayout();
			ContentLayout.SuspendLayout();
			SuspendLayout();
			// 
			// AddOGGFilesRecursivly
			// 
			AddOGGFilesRecursivly.BackColor = Color.FromArgb(34, 34, 40);
			AddOGGFilesRecursivly.Cursor = Cursors.Hand;
			AddOGGFilesRecursivly.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 62);
			AddOGGFilesRecursivly.FlatStyle = FlatStyle.Flat;
			AddOGGFilesRecursivly.Font = new Font("Segoe UI", 9.5F);
			AddOGGFilesRecursivly.ForeColor = Color.FromArgb(235, 235, 240);
			AddOGGFilesRecursivly.Location = new Point(0, 0);
			AddOGGFilesRecursivly.Margin = new Padding(0, 0, 10, 10);
			AddOGGFilesRecursivly.Name = "AddOGGFilesRecursivly";
			AddOGGFilesRecursivly.Padding = new Padding(12, 8, 12, 8);
			AddOGGFilesRecursivly.Size = new Size(110, 62);
			AddOGGFilesRecursivly.TabIndex = 0;
			AddOGGFilesRecursivly.Text = "Add folder (recursive)";
			AddOGGFilesRecursivly.UseVisualStyleBackColor = false;
			AddOGGFilesRecursivly.Click += AddOGGFilesRecursivly_Click;
			// 
			// CheckedOGGListBox
			// 
			CheckedOGGListBox.BackColor = Color.FromArgb(30, 30, 36);
			CheckedOGGListBox.BorderStyle = BorderStyle.FixedSingle;
			CheckedOGGListBox.Dock = DockStyle.Fill;
			CheckedOGGListBox.Font = new Font("Segoe UI", 9.25F);
			CheckedOGGListBox.ForeColor = Color.FromArgb(235, 235, 240);
			CheckedOGGListBox.FormattingEnabled = true;
			CheckedOGGListBox.IntegralHeight = false;
			CheckedOGGListBox.Location = new Point(0, 0);
			CheckedOGGListBox.Margin = new Padding(0, 0, 12, 0);
			CheckedOGGListBox.Name = "CheckedOGGListBox";
			CheckedOGGListBox.Size = new Size(452, 520);
			CheckedOGGListBox.TabIndex = 1;
			// 
			// AddFilesSelectOGG
			// 
			AddFilesSelectOGG.BackColor = Color.FromArgb(34, 34, 40);
			AddFilesSelectOGG.Cursor = Cursors.Hand;
			AddFilesSelectOGG.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 62);
			AddFilesSelectOGG.FlatStyle = FlatStyle.Flat;
			AddFilesSelectOGG.Font = new Font("Segoe UI", 9.5F);
			AddFilesSelectOGG.ForeColor = Color.FromArgb(235, 235, 240);
			AddFilesSelectOGG.Location = new Point(120, 0);
			AddFilesSelectOGG.Margin = new Padding(0, 0, 10, 10);
			AddFilesSelectOGG.Name = "AddFilesSelectOGG";
			AddFilesSelectOGG.Padding = new Padding(12, 8, 12, 8);
			AddFilesSelectOGG.Size = new Size(105, 62);
			AddFilesSelectOGG.TabIndex = 2;
			AddFilesSelectOGG.Text = "Add OGG files";
			AddFilesSelectOGG.UseVisualStyleBackColor = false;
			AddFilesSelectOGG.Click += AddFilesSelectOGG_Click;
			// 
			// SelectDeselectAll
			// 
			SelectDeselectAll.BackColor = Color.FromArgb(34, 34, 40);
			SelectDeselectAll.Cursor = Cursors.Hand;
			SelectDeselectAll.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 62);
			SelectDeselectAll.FlatStyle = FlatStyle.Flat;
			SelectDeselectAll.Font = new Font("Segoe UI", 9.5F);
			SelectDeselectAll.ForeColor = Color.FromArgb(235, 235, 240);
			SelectDeselectAll.Location = new Point(350, 0);
			SelectDeselectAll.Margin = new Padding(0, 0, 10, 10);
			SelectDeselectAll.Name = "SelectDeselectAll";
			SelectDeselectAll.Padding = new Padding(12, 8, 12, 8);
			SelectDeselectAll.Size = new Size(102, 62);
			SelectDeselectAll.TabIndex = 3;
			SelectDeselectAll.Text = "Check All";
			SelectDeselectAll.UseVisualStyleBackColor = false;
			SelectDeselectAll.Click += SelectDeselectAll_Click;
			// 
			// RemoveDuplicates
			// 
			RemoveDuplicates.BackColor = Color.FromArgb(34, 34, 40);
			RemoveDuplicates.Cursor = Cursors.Hand;
			RemoveDuplicates.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 62);
			RemoveDuplicates.FlatStyle = FlatStyle.Flat;
			RemoveDuplicates.Font = new Font("Segoe UI", 9.5F);
			RemoveDuplicates.ForeColor = Color.FromArgb(235, 235, 240);
			RemoveDuplicates.Location = new Point(235, 0);
			RemoveDuplicates.Margin = new Padding(0, 0, 10, 10);
			RemoveDuplicates.Name = "RemoveDuplicates";
			RemoveDuplicates.Padding = new Padding(12, 8, 12, 8);
			RemoveDuplicates.Size = new Size(105, 62);
			RemoveDuplicates.TabIndex = 4;
			RemoveDuplicates.Text = "Remove duplicates";
			RemoveDuplicates.UseVisualStyleBackColor = false;
			RemoveDuplicates.Click += RemoveDuplicates_Click;
			// 
			// OutputTextBox
			// 
			OutputTextBox.BackColor = Color.FromArgb(20, 20, 24);
			OutputTextBox.BorderStyle = BorderStyle.FixedSingle;
			OutputTextBox.Dock = DockStyle.Right;
			OutputTextBox.Font = new Font("Consolas", 9.5F);
			OutputTextBox.ForeColor = Color.FromArgb(220, 220, 225);
			OutputTextBox.Location = new Point(464, 0);
			OutputTextBox.Margin = new Padding(0);
			OutputTextBox.Multiline = true;
			OutputTextBox.Name = "OutputTextBox";
			OutputTextBox.ScrollBars = ScrollBars.Vertical;
			OutputTextBox.Size = new Size(488, 520);
			OutputTextBox.TabIndex = 6;
			// 
			// SaveToMemoryButton
			// 
			SaveToMemoryButton.Anchor = AnchorStyles.None;
			SaveToMemoryButton.BackColor = Color.FromArgb(34, 34, 40);
			SaveToMemoryButton.Cursor = Cursors.Hand;
			SaveToMemoryButton.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 62);
			SaveToMemoryButton.FlatStyle = FlatStyle.Flat;
			SaveToMemoryButton.Font = new Font("Segoe UI", 9.5F);
			SaveToMemoryButton.ForeColor = Color.FromArgb(235, 235, 240);
			SaveToMemoryButton.Location = new Point(716, 20);
			SaveToMemoryButton.Margin = new Padding(0, 20, 10, 10);
			SaveToMemoryButton.Name = "SaveToMemoryButton";
			SaveToMemoryButton.Padding = new Padding(12, 8, 12, 8);
			SaveToMemoryButton.Size = new Size(113, 45);
			SaveToMemoryButton.TabIndex = 7;
			SaveToMemoryButton.Text = "Save results";
			SaveToMemoryButton.UseVisualStyleBackColor = false;
			// 
			// ExecuteProcessBtn
			// 
			ExecuteProcessBtn.BackColor = Color.FromArgb(90, 66, 245);
			ExecuteProcessBtn.Cursor = Cursors.Hand;
			ExecuteProcessBtn.FlatAppearance.BorderSize = 0;
			ExecuteProcessBtn.FlatStyle = FlatStyle.Flat;
			ExecuteProcessBtn.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
			ExecuteProcessBtn.ForeColor = Color.White;
			ExecuteProcessBtn.Location = new Point(562, 20);
			ExecuteProcessBtn.Margin = new Padding(100, 20, 10, 10);
			ExecuteProcessBtn.Name = "ExecuteProcessBtn";
			ExecuteProcessBtn.Padding = new Padding(14, 9, 14, 9);
			ExecuteProcessBtn.Size = new Size(144, 45);
			ExecuteProcessBtn.TabIndex = 8;
			ExecuteProcessBtn.Text = "Convert to WAV";
			ExecuteProcessBtn.UseVisualStyleBackColor = false;
			ExecuteProcessBtn.Click += ExecuteProcessBtn_Click;
			// 
			// RootLayout
			// 
			RootLayout.BackColor = Color.FromArgb(24, 24, 28);
			RootLayout.ColumnCount = 1;
			RootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			RootLayout.Controls.Add(TopBar, 0, 0);
			RootLayout.Controls.Add(ContentLayout, 0, 1);
			RootLayout.Dock = DockStyle.Fill;
			RootLayout.Location = new Point(0, 0);
			RootLayout.Margin = new Padding(0);
			RootLayout.Name = "RootLayout";
			RootLayout.Padding = new Padding(14);
			RootLayout.RowCount = 2;
			RootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
			RootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			RootLayout.Size = new Size(980, 620);
			RootLayout.TabIndex = 100;
			// 
			// TopBar
			// 
			TopBar.BackColor = Color.Transparent;
			TopBar.Controls.Add(AddOGGFilesRecursivly);
			TopBar.Controls.Add(AddFilesSelectOGG);
			TopBar.Controls.Add(RemoveDuplicates);
			TopBar.Controls.Add(SelectDeselectAll);
			TopBar.Controls.Add(ExecuteProcessBtn);
			TopBar.Controls.Add(SaveToMemoryButton);
			TopBar.Dock = DockStyle.Fill;
			TopBar.Location = new Point(14, 14);
			TopBar.Margin = new Padding(0);
			TopBar.Name = "TopBar";
			TopBar.Size = new Size(952, 72);
			TopBar.TabIndex = 101;
			// 
			// ContentLayout
			// 
			ContentLayout.BackColor = Color.Transparent;
			ContentLayout.ColumnCount = 2;
			ContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.7394943F));
			ContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.2605057F));
			ContentLayout.Controls.Add(CheckedOGGListBox, 0, 0);
			ContentLayout.Controls.Add(OutputTextBox, 1, 0);
			ContentLayout.Dock = DockStyle.Fill;
			ContentLayout.Location = new Point(14, 86);
			ContentLayout.Margin = new Padding(0);
			ContentLayout.Name = "ContentLayout";
			ContentLayout.RowCount = 1;
			ContentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			ContentLayout.Size = new Size(952, 520);
			ContentLayout.TabIndex = 102;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(24, 24, 28);
			ClientSize = new Size(980, 620);
			Controls.Add(RootLayout);
			Font = new Font("Segoe UI", 9F);
			ForeColor = Color.FromArgb(235, 235, 240);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "IV OGG to WAV";
			Load += MainForm_Load;
			RootLayout.ResumeLayout(false);
			TopBar.ResumeLayout(false);
			ContentLayout.ResumeLayout(false);
			ContentLayout.PerformLayout();
			ResumeLayout(false);
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

		private TableLayoutPanel RootLayout;
		private FlowLayoutPanel TopBar;
		private TableLayoutPanel ContentLayout;
	}
}
