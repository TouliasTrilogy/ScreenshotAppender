namespace ScreenshotAppender
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Show_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Disable_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ClearClipboard_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveClipboardAs_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.Exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.Enabled_checkBox = new System.Windows.Forms.CheckBox();
			this.ClearWhenPasteCtrlV_checkBox = new System.Windows.Forms.CheckBox();
			this.CaptureKeys_comboBox = new System.Windows.Forms.ComboBox();
			this.CaptureCtrl_checkBox = new System.Windows.Forms.CheckBox();
			this.CaptureAlt_checkBox = new System.Windows.Forms.CheckBox();
			this.CaptureShift_checkBox = new System.Windows.Forms.CheckBox();
			this.StartWithWindows_checkBox = new System.Windows.Forms.CheckBox();
			this.CaptureOnTrayClick_checkBox = new System.Windows.Forms.CheckBox();
			this.PreventProcessingCaptureKey_checkBox = new System.Windows.Forms.CheckBox();
			this.MultiScreen_checkBox = new System.Windows.Forms.CheckBox();
			this.ClearWhenPasteShiftIns_checkBox = new System.Windows.Forms.CheckBox();
			this.ComposeVertically_radioButton = new System.Windows.Forms.RadioButton();
			this.ComposeHorizontally_radioButton = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.PasteKeys_comboBox = new System.Windows.Forms.ComboBox();
			this.PasteAlt_checkBox = new System.Windows.Forms.CheckBox();
			this.PasteCtrl_checkBox = new System.Windows.Forms.CheckBox();
			this.PasteShift_checkBox = new System.Windows.Forms.CheckBox();
			this.PreventProcessingPasteKey_checkBox = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ReduceColors_comboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.ImageSize_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.StackSize_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.Stack_checkBox = new System.Windows.Forms.CheckBox();
			this.SaveClipboard_button = new System.Windows.Forms.Button();
			this.ClearClipboard_button = new System.Windows.Forms.Button();
			this.contextMenuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageSize_numericUpDown)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StackSize_numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Screenshot Appender";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Show_ToolStripMenuItem,
            this.Disable_ToolStripMenuItem,
            this.toolStripSeparator2,
            this.ClearClipboard_ToolStripMenuItem,
            this.SaveClipboardAs_toolStripMenuItem,
            this.toolStripSeparator1,
            this.Exit_ToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(229, 126);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
			// 
			// Show_ToolStripMenuItem
			// 
			this.Show_ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.Show_ToolStripMenuItem.Name = "Show_ToolStripMenuItem";
			this.Show_ToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.Show_ToolStripMenuItem.Text = "Show";
			this.Show_ToolStripMenuItem.Click += new System.EventHandler(this.Show_ToolStripMenuItem_Click);
			// 
			// Disable_ToolStripMenuItem
			// 
			this.Disable_ToolStripMenuItem.Name = "Disable_ToolStripMenuItem";
			this.Disable_ToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.Disable_ToolStripMenuItem.Text = "Disable Screenshot Appender";
			this.Disable_ToolStripMenuItem.Click += new System.EventHandler(this.Disable_ToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
			// 
			// ClearClipboard_ToolStripMenuItem
			// 
			this.ClearClipboard_ToolStripMenuItem.Name = "ClearClipboard_ToolStripMenuItem";
			this.ClearClipboard_ToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.ClearClipboard_ToolStripMenuItem.Text = "Clear clipboard";
			this.ClearClipboard_ToolStripMenuItem.Click += new System.EventHandler(this.ClearClipboard_ToolStripMenuItem_Click);
			// 
			// SaveClipboardAs_toolStripMenuItem
			// 
			this.SaveClipboardAs_toolStripMenuItem.Name = "SaveClipboardAs_toolStripMenuItem";
			this.SaveClipboardAs_toolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.SaveClipboardAs_toolStripMenuItem.Text = "Save clipboard as...";
			this.SaveClipboardAs_toolStripMenuItem.Click += new System.EventHandler(this.SaveClipboardAs_toolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
			// 
			// Exit_ToolStripMenuItem
			// 
			this.Exit_ToolStripMenuItem.Name = "Exit_ToolStripMenuItem";
			this.Exit_ToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.Exit_ToolStripMenuItem.Text = "Exit";
			this.Exit_ToolStripMenuItem.Click += new System.EventHandler(this.Exit_ToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "png";
			this.saveFileDialog.FileName = "clipboard.png";
			this.saveFileDialog.Filter = "Portable Network Graphics (*.png)|*.png|All files (*.*)|*.*";
			this.saveFileDialog.Title = "Save clipboard as image";
			// 
			// Enabled_checkBox
			// 
			this.Enabled_checkBox.AutoSize = true;
			this.Enabled_checkBox.Checked = true;
			this.Enabled_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Enabled_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Enabled_checkBox.Location = new System.Drawing.Point(15, 42);
			this.Enabled_checkBox.Name = "Enabled_checkBox";
			this.Enabled_checkBox.Size = new System.Drawing.Size(162, 17);
			this.Enabled_checkBox.TabIndex = 1;
			this.Enabled_checkBox.Text = "Enable Screenshot Appender";
			this.Enabled_checkBox.UseVisualStyleBackColor = true;
			// 
			// ClearWhenPasteCtrlV_checkBox
			// 
			this.ClearWhenPasteCtrlV_checkBox.AutoSize = true;
			this.ClearWhenPasteCtrlV_checkBox.Checked = true;
			this.ClearWhenPasteCtrlV_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ClearWhenPasteCtrlV_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ClearWhenPasteCtrlV_checkBox.Location = new System.Drawing.Point(15, 65);
			this.ClearWhenPasteCtrlV_checkBox.Name = "ClearWhenPasteCtrlV_checkBox";
			this.ClearWhenPasteCtrlV_checkBox.Size = new System.Drawing.Size(240, 17);
			this.ClearWhenPasteCtrlV_checkBox.TabIndex = 2;
			this.ClearWhenPasteCtrlV_checkBox.Text = "Clear collected images when paste with Ctrl+V";
			this.ClearWhenPasteCtrlV_checkBox.UseVisualStyleBackColor = true;
			// 
			// CaptureKeys_comboBox
			// 
			this.CaptureKeys_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CaptureKeys_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CaptureKeys_comboBox.FormattingEnabled = true;
			this.CaptureKeys_comboBox.Location = new System.Drawing.Point(6, 19);
			this.CaptureKeys_comboBox.Name = "CaptureKeys_comboBox";
			this.CaptureKeys_comboBox.Size = new System.Drawing.Size(138, 21);
			this.CaptureKeys_comboBox.TabIndex = 0;
			// 
			// CaptureCtrl_checkBox
			// 
			this.CaptureCtrl_checkBox.AutoSize = true;
			this.CaptureCtrl_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CaptureCtrl_checkBox.Location = new System.Drawing.Point(153, 22);
			this.CaptureCtrl_checkBox.Name = "CaptureCtrl_checkBox";
			this.CaptureCtrl_checkBox.Size = new System.Drawing.Size(38, 17);
			this.CaptureCtrl_checkBox.TabIndex = 1;
			this.CaptureCtrl_checkBox.Text = "Ctrl";
			this.CaptureCtrl_checkBox.UseVisualStyleBackColor = true;
			// 
			// CaptureAlt_checkBox
			// 
			this.CaptureAlt_checkBox.AutoSize = true;
			this.CaptureAlt_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CaptureAlt_checkBox.Location = new System.Drawing.Point(200, 22);
			this.CaptureAlt_checkBox.Name = "CaptureAlt_checkBox";
			this.CaptureAlt_checkBox.Size = new System.Drawing.Size(35, 17);
			this.CaptureAlt_checkBox.TabIndex = 2;
			this.CaptureAlt_checkBox.Text = "Alt";
			this.CaptureAlt_checkBox.UseVisualStyleBackColor = true;
			// 
			// CaptureShift_checkBox
			// 
			this.CaptureShift_checkBox.AutoSize = true;
			this.CaptureShift_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CaptureShift_checkBox.Location = new System.Drawing.Point(244, 22);
			this.CaptureShift_checkBox.Name = "CaptureShift_checkBox";
			this.CaptureShift_checkBox.Size = new System.Drawing.Size(44, 17);
			this.CaptureShift_checkBox.TabIndex = 3;
			this.CaptureShift_checkBox.Text = "Shift";
			this.CaptureShift_checkBox.UseVisualStyleBackColor = true;
			// 
			// StartWithWindows_checkBox
			// 
			this.StartWithWindows_checkBox.AutoSize = true;
			this.StartWithWindows_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.StartWithWindows_checkBox.Location = new System.Drawing.Point(15, 19);
			this.StartWithWindows_checkBox.Name = "StartWithWindows_checkBox";
			this.StartWithWindows_checkBox.Size = new System.Drawing.Size(114, 17);
			this.StartWithWindows_checkBox.TabIndex = 0;
			this.StartWithWindows_checkBox.Text = "Start with Windows";
			this.StartWithWindows_checkBox.UseVisualStyleBackColor = true;
			// 
			// CaptureOnTrayClick_checkBox
			// 
			this.CaptureOnTrayClick_checkBox.AutoSize = true;
			this.CaptureOnTrayClick_checkBox.Checked = true;
			this.CaptureOnTrayClick_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CaptureOnTrayClick_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CaptureOnTrayClick_checkBox.Location = new System.Drawing.Point(15, 111);
			this.CaptureOnTrayClick_checkBox.Name = "CaptureOnTrayClick_checkBox";
			this.CaptureOnTrayClick_checkBox.Size = new System.Drawing.Size(219, 17);
			this.CaptureOnTrayClick_checkBox.TabIndex = 4;
			this.CaptureOnTrayClick_checkBox.Text = "Capture screen on single click in tray icon";
			this.CaptureOnTrayClick_checkBox.UseVisualStyleBackColor = true;
			// 
			// PreventProcessingCaptureKey_checkBox
			// 
			this.PreventProcessingCaptureKey_checkBox.AutoSize = true;
			this.PreventProcessingCaptureKey_checkBox.Checked = true;
			this.PreventProcessingCaptureKey_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.PreventProcessingCaptureKey_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PreventProcessingCaptureKey_checkBox.Location = new System.Drawing.Point(6, 46);
			this.PreventProcessingCaptureKey_checkBox.Name = "PreventProcessingCaptureKey_checkBox";
			this.PreventProcessingCaptureKey_checkBox.Size = new System.Drawing.Size(197, 17);
			this.PreventProcessingCaptureKey_checkBox.TabIndex = 4;
			this.PreventProcessingCaptureKey_checkBox.Text = "Prevent processing key after capture";
			this.PreventProcessingCaptureKey_checkBox.UseVisualStyleBackColor = true;
			// 
			// MultiScreen_checkBox
			// 
			this.MultiScreen_checkBox.AutoSize = true;
			this.MultiScreen_checkBox.Checked = true;
			this.MultiScreen_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MultiScreen_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MultiScreen_checkBox.Location = new System.Drawing.Point(15, 134);
			this.MultiScreen_checkBox.Name = "MultiScreen_checkBox";
			this.MultiScreen_checkBox.Size = new System.Drawing.Size(175, 17);
			this.MultiScreen_checkBox.TabIndex = 5;
			this.MultiScreen_checkBox.Text = "Enable multiple sceeens support";
			this.MultiScreen_checkBox.UseVisualStyleBackColor = true;
			// 
			// ClearWhenPasteShiftIns_checkBox
			// 
			this.ClearWhenPasteShiftIns_checkBox.AutoSize = true;
			this.ClearWhenPasteShiftIns_checkBox.Checked = true;
			this.ClearWhenPasteShiftIns_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ClearWhenPasteShiftIns_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ClearWhenPasteShiftIns_checkBox.Location = new System.Drawing.Point(15, 88);
			this.ClearWhenPasteShiftIns_checkBox.Name = "ClearWhenPasteShiftIns_checkBox";
			this.ClearWhenPasteShiftIns_checkBox.Size = new System.Drawing.Size(253, 17);
			this.ClearWhenPasteShiftIns_checkBox.TabIndex = 3;
			this.ClearWhenPasteShiftIns_checkBox.Text = "Clear collected images when paste with Shift+Ins";
			this.ClearWhenPasteShiftIns_checkBox.UseVisualStyleBackColor = true;
			// 
			// ComposeVertically_radioButton
			// 
			this.ComposeVertically_radioButton.AutoSize = true;
			this.ComposeVertically_radioButton.Checked = true;
			this.ComposeVertically_radioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ComposeVertically_radioButton.Location = new System.Drawing.Point(58, 21);
			this.ComposeVertically_radioButton.Name = "ComposeVertically_radioButton";
			this.ComposeVertically_radioButton.Size = new System.Drawing.Size(66, 17);
			this.ComposeVertically_radioButton.TabIndex = 0;
			this.ComposeVertically_radioButton.TabStop = true;
			this.ComposeVertically_radioButton.Text = "Vertically";
			this.ComposeVertically_radioButton.UseVisualStyleBackColor = true;
			this.ComposeVertically_radioButton.CheckedChanged += new System.EventHandler(this.ComposeVertically_radioButton_CheckedChanged);
			// 
			// ComposeHorizontally_radioButton
			// 
			this.ComposeHorizontally_radioButton.AutoSize = true;
			this.ComposeHorizontally_radioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ComposeHorizontally_radioButton.Location = new System.Drawing.Point(163, 21);
			this.ComposeHorizontally_radioButton.Name = "ComposeHorizontally_radioButton";
			this.ComposeHorizontally_radioButton.Size = new System.Drawing.Size(78, 17);
			this.ComposeHorizontally_radioButton.TabIndex = 1;
			this.ComposeHorizontally_radioButton.Text = "Horizontally";
			this.ComposeHorizontally_radioButton.UseVisualStyleBackColor = true;
			this.ComposeHorizontally_radioButton.CheckedChanged += new System.EventHandler(this.ComposeVertically_radioButton_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CaptureKeys_comboBox);
			this.groupBox1.Controls.Add(this.CaptureAlt_checkBox);
			this.groupBox1.Controls.Add(this.CaptureCtrl_checkBox);
			this.groupBox1.Controls.Add(this.CaptureShift_checkBox);
			this.groupBox1.Controls.Add(this.PreventProcessingCaptureKey_checkBox);
			this.groupBox1.Location = new System.Drawing.Point(15, 210);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(299, 70);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Screen capture key";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.PasteKeys_comboBox);
			this.groupBox2.Controls.Add(this.PasteAlt_checkBox);
			this.groupBox2.Controls.Add(this.PasteCtrl_checkBox);
			this.groupBox2.Controls.Add(this.PasteShift_checkBox);
			this.groupBox2.Controls.Add(this.PreventProcessingPasteKey_checkBox);
			this.groupBox2.Location = new System.Drawing.Point(15, 286);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(299, 70);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Custom paste key";
			// 
			// PasteKeys_comboBox
			// 
			this.PasteKeys_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PasteKeys_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PasteKeys_comboBox.FormattingEnabled = true;
			this.PasteKeys_comboBox.Location = new System.Drawing.Point(6, 19);
			this.PasteKeys_comboBox.Name = "PasteKeys_comboBox";
			this.PasteKeys_comboBox.Size = new System.Drawing.Size(138, 21);
			this.PasteKeys_comboBox.TabIndex = 0;
			// 
			// PasteAlt_checkBox
			// 
			this.PasteAlt_checkBox.AutoSize = true;
			this.PasteAlt_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PasteAlt_checkBox.Location = new System.Drawing.Point(200, 22);
			this.PasteAlt_checkBox.Name = "PasteAlt_checkBox";
			this.PasteAlt_checkBox.Size = new System.Drawing.Size(35, 17);
			this.PasteAlt_checkBox.TabIndex = 2;
			this.PasteAlt_checkBox.Text = "Alt";
			this.PasteAlt_checkBox.UseVisualStyleBackColor = true;
			// 
			// PasteCtrl_checkBox
			// 
			this.PasteCtrl_checkBox.AutoSize = true;
			this.PasteCtrl_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PasteCtrl_checkBox.Location = new System.Drawing.Point(153, 22);
			this.PasteCtrl_checkBox.Name = "PasteCtrl_checkBox";
			this.PasteCtrl_checkBox.Size = new System.Drawing.Size(38, 17);
			this.PasteCtrl_checkBox.TabIndex = 1;
			this.PasteCtrl_checkBox.Text = "Ctrl";
			this.PasteCtrl_checkBox.UseVisualStyleBackColor = true;
			// 
			// PasteShift_checkBox
			// 
			this.PasteShift_checkBox.AutoSize = true;
			this.PasteShift_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PasteShift_checkBox.Location = new System.Drawing.Point(244, 22);
			this.PasteShift_checkBox.Name = "PasteShift_checkBox";
			this.PasteShift_checkBox.Size = new System.Drawing.Size(44, 17);
			this.PasteShift_checkBox.TabIndex = 3;
			this.PasteShift_checkBox.Text = "Shift";
			this.PasteShift_checkBox.UseVisualStyleBackColor = true;
			// 
			// PreventProcessingPasteKey_checkBox
			// 
			this.PreventProcessingPasteKey_checkBox.AutoSize = true;
			this.PreventProcessingPasteKey_checkBox.Checked = true;
			this.PreventProcessingPasteKey_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.PreventProcessingPasteKey_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PreventProcessingPasteKey_checkBox.Location = new System.Drawing.Point(6, 46);
			this.PreventProcessingPasteKey_checkBox.Name = "PreventProcessingPasteKey_checkBox";
			this.PreventProcessingPasteKey_checkBox.Size = new System.Drawing.Size(187, 17);
			this.PreventProcessingPasteKey_checkBox.TabIndex = 4;
			this.PreventProcessingPasteKey_checkBox.Text = "Prevent processing key after paste";
			this.PreventProcessingPasteKey_checkBox.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.ReduceColors_comboBox);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.ImageSize_numericUpDown);
			this.groupBox4.Controls.Add(this.groupBox3);
			this.groupBox4.Controls.Add(this.StartWithWindows_checkBox);
			this.groupBox4.Controls.Add(this.Enabled_checkBox);
			this.groupBox4.Controls.Add(this.ClearWhenPasteCtrlV_checkBox);
			this.groupBox4.Controls.Add(this.groupBox2);
			this.groupBox4.Controls.Add(this.groupBox1);
			this.groupBox4.Controls.Add(this.CaptureOnTrayClick_checkBox);
			this.groupBox4.Controls.Add(this.MultiScreen_checkBox);
			this.groupBox4.Controls.Add(this.ClearWhenPasteShiftIns_checkBox);
			this.groupBox4.Location = new System.Drawing.Point(11, 10);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(328, 448);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Options";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(177, 187);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(15, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "%";
			// 
			// ReduceColors_comboBox
			// 
			this.ReduceColors_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ReduceColors_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ReduceColors_comboBox.FormattingEnabled = true;
			this.ReduceColors_comboBox.Items.AddRange(new object[] {
            "Leave as is",
            "24 bit per pixel",
            "16 bit per pixel"});
			this.ReduceColors_comboBox.Location = new System.Drawing.Point(191, 156);
			this.ReduceColors_comboBox.Name = "ReduceColors_comboBox";
			this.ReduceColors_comboBox.Size = new System.Drawing.Size(123, 21);
			this.ReduceColors_comboBox.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 186);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Result image size:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 159);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(173, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Reduce colors of clipboard images:";
			// 
			// ImageSize_numericUpDown
			// 
			this.ImageSize_numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageSize_numericUpDown.Location = new System.Drawing.Point(110, 183);
			this.ImageSize_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ImageSize_numericUpDown.Name = "ImageSize_numericUpDown";
			this.ImageSize_numericUpDown.Size = new System.Drawing.Size(61, 20);
			this.ImageSize_numericUpDown.TabIndex = 9;
			this.ImageSize_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ImageSize_numericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.StackSize_numericUpDown);
			this.groupBox3.Controls.Add(this.Stack_checkBox);
			this.groupBox3.Controls.Add(this.ComposeVertically_radioButton);
			this.groupBox3.Controls.Add(this.ComposeHorizontally_radioButton);
			this.groupBox3.Location = new System.Drawing.Point(15, 362);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(299, 76);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Image composign";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(189, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "in column";
			// 
			// StackSize_numericUpDown
			// 
			this.StackSize_numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.StackSize_numericUpDown.Location = new System.Drawing.Point(118, 48);
			this.StackSize_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.StackSize_numericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.StackSize_numericUpDown.Name = "StackSize_numericUpDown";
			this.StackSize_numericUpDown.Size = new System.Drawing.Size(61, 20);
			this.StackSize_numericUpDown.TabIndex = 3;
			this.StackSize_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.StackSize_numericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// Stack_checkBox
			// 
			this.Stack_checkBox.AutoSize = true;
			this.Stack_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Stack_checkBox.Location = new System.Drawing.Point(58, 49);
			this.Stack_checkBox.Name = "Stack_checkBox";
			this.Stack_checkBox.Size = new System.Drawing.Size(51, 17);
			this.Stack_checkBox.TabIndex = 2;
			this.Stack_checkBox.Text = "Stack";
			this.Stack_checkBox.UseVisualStyleBackColor = true;
			// 
			// SaveClipboard_button
			// 
			this.SaveClipboard_button.Enabled = false;
			this.SaveClipboard_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveClipboard_button.Location = new System.Drawing.Point(190, 473);
			this.SaveClipboard_button.Name = "SaveClipboard_button";
			this.SaveClipboard_button.Size = new System.Drawing.Size(149, 23);
			this.SaveClipboard_button.TabIndex = 2;
			this.SaveClipboard_button.Text = "Save clipboard as...";
			this.SaveClipboard_button.UseVisualStyleBackColor = true;
			this.SaveClipboard_button.Click += new System.EventHandler(this.SaveClipboardAs_toolStripMenuItem_Click);
			// 
			// ClearClipboard_button
			// 
			this.ClearClipboard_button.Enabled = false;
			this.ClearClipboard_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ClearClipboard_button.Location = new System.Drawing.Point(11, 473);
			this.ClearClipboard_button.Name = "ClearClipboard_button";
			this.ClearClipboard_button.Size = new System.Drawing.Size(149, 23);
			this.ClearClipboard_button.TabIndex = 1;
			this.ClearClipboard_button.Text = "Clear clipboard";
			this.ClearClipboard_button.UseVisualStyleBackColor = true;
			this.ClearClipboard_button.Click += new System.EventHandler(this.ClearClipboard_ToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(351, 508);
			this.Controls.Add(this.SaveClipboard_button);
			this.Controls.Add(this.ClearClipboard_button);
			this.Controls.Add(this.groupBox4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Screenshot Appender";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.contextMenuStrip.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageSize_numericUpDown)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.StackSize_numericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem Exit_ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveClipboardAs_toolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ClearClipboard_ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem Show_ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.CheckBox Enabled_checkBox;
		private System.Windows.Forms.CheckBox ClearWhenPasteCtrlV_checkBox;
		private System.Windows.Forms.ComboBox CaptureKeys_comboBox;
		private System.Windows.Forms.CheckBox CaptureCtrl_checkBox;
		private System.Windows.Forms.CheckBox CaptureAlt_checkBox;
		private System.Windows.Forms.CheckBox CaptureShift_checkBox;
		private System.Windows.Forms.CheckBox StartWithWindows_checkBox;
		private System.Windows.Forms.CheckBox CaptureOnTrayClick_checkBox;
		private System.Windows.Forms.CheckBox PreventProcessingCaptureKey_checkBox;
		private System.Windows.Forms.CheckBox MultiScreen_checkBox;
		private System.Windows.Forms.CheckBox ClearWhenPasteShiftIns_checkBox;
		private System.Windows.Forms.ToolStripMenuItem Disable_ToolStripMenuItem;
		private System.Windows.Forms.RadioButton ComposeVertically_radioButton;
		private System.Windows.Forms.RadioButton ComposeHorizontally_radioButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox PasteKeys_comboBox;
		private System.Windows.Forms.CheckBox PasteAlt_checkBox;
		private System.Windows.Forms.CheckBox PasteCtrl_checkBox;
		private System.Windows.Forms.CheckBox PasteShift_checkBox;
		private System.Windows.Forms.CheckBox PreventProcessingPasteKey_checkBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown StackSize_numericUpDown;
		private System.Windows.Forms.CheckBox Stack_checkBox;
		private System.Windows.Forms.Button SaveClipboard_button;
		private System.Windows.Forms.ComboBox ReduceColors_comboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button ClearClipboard_button;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown ImageSize_numericUpDown;
	}
}

