
namespace MapleScreenshotBackup.Forms
{
    partial class MainForm
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
            System.Windows.Forms.ToolStrip mainToolStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.StatusStrip mainStatusStrip;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Panel panel2;
            this.exportLogButton = new System.Windows.Forms.ToolStripButton();
            this.newReleaseButton = new System.Windows.Forms.ToolStripButton();
            this.versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.githubLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.screenshotDirInput = new System.Windows.Forms.TextBox();
            this.screenshotDirSelectButton = new System.Windows.Forms.Button();
            this.backupDirInput = new System.Windows.Forms.TextBox();
            this.backupDirSelectButton = new System.Windows.Forms.Button();
            this.canDeleteCheckBox = new System.Windows.Forms.CheckBox();
            this.screenshotsFindButton = new System.Windows.Forms.Button();
            this.openBackupDirButton = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.backupLog = new System.Windows.Forms.ListBox();
            this.backupProgressBar = new System.Windows.Forms.ProgressBar();
            this.dirSelectDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveLogDialog = new System.Windows.Forms.SaveFileDialog();
            mainToolStrip = new System.Windows.Forms.ToolStrip();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            mainStatusStrip = new System.Windows.Forms.StatusStrip();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            mainToolStrip.SuspendLayout();
            mainStatusStrip.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            mainToolStrip.BackColor = System.Drawing.SystemColors.Window;
            mainToolStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportLogButton,
            this.newReleaseButton});
            mainToolStrip.Location = new System.Drawing.Point(0, 0);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            mainToolStrip.Size = new System.Drawing.Size(668, 25);
            mainToolStrip.TabIndex = 0;
            mainToolStrip.Text = "mainToolStrip";
            // 
            // exportLogButton
            // 
            this.exportLogButton.Image = ((System.Drawing.Image)(resources.GetObject("exportLogButton.Image")));
            this.exportLogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportLogButton.Name = "exportLogButton";
            this.exportLogButton.Size = new System.Drawing.Size(92, 22);
            this.exportLogButton.Text = "&Export Logs";
            this.exportLogButton.Click += new System.EventHandler(this.OnExportLogButtonClicked);
            // 
            // newReleaseButton
            // 
            this.newReleaseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.newReleaseButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.newReleaseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newReleaseButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.newReleaseButton.Image = ((System.Drawing.Image)(resources.GetObject("newReleaseButton.Image")));
            this.newReleaseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newReleaseButton.Name = "newReleaseButton";
            this.newReleaseButton.Size = new System.Drawing.Size(115, 22);
            this.newReleaseButton.Text = "&New release found";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(12, 15);
            label2.Margin = new System.Windows.Forms.Padding(0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(101, 15);
            label2.TabIndex = 0;
            label2.Text = "Screenshot Folder";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(31, 43);
            label1.Margin = new System.Windows.Forms.Padding(0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(82, 15);
            label1.TabIndex = 0;
            label1.Text = "Backup Folder";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.BackColor = System.Drawing.SystemColors.Window;
            mainStatusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            mainStatusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionLabel,
            this.githubLink});
            mainStatusStrip.Location = new System.Drawing.Point(0, 383);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.Size = new System.Drawing.Size(668, 22);
            mainStatusStrip.SizingGrip = false;
            mainStatusStrip.TabIndex = 0;
            mainStatusStrip.Text = "statusStrip1";
            // 
            // versionLabel
            // 
            this.versionLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(146, 17);
            this.versionLabel.Text = "Maple Screenshot Backup";
            // 
            // githubLink
            // 
            this.githubLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.githubLink.IsLink = true;
            this.githubLink.LinkColor = System.Drawing.Color.LimeGreen;
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(45, 17);
            this.githubLink.Text = "GitHub";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(panel2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 25);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(668, 358);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.Control;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(this.screenshotDirInput);
            panel2.Controls.Add(this.screenshotDirSelectButton);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(this.backupDirInput);
            panel2.Controls.Add(this.backupDirSelectButton);
            panel2.Controls.Add(this.canDeleteCheckBox);
            panel2.Controls.Add(this.screenshotsFindButton);
            panel2.Controls.Add(this.openBackupDirButton);
            panel2.Controls.Add(this.backupButton);
            panel2.Controls.Add(this.backupLog);
            panel2.Controls.Add(this.backupProgressBar);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(668, 358);
            panel2.TabIndex = 1;
            // 
            // screenshotDirInput
            // 
            this.screenshotDirInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.screenshotDirInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.screenshotDirInput.BackColor = System.Drawing.SystemColors.Window;
            this.screenshotDirInput.Location = new System.Drawing.Point(116, 12);
            this.screenshotDirInput.Name = "screenshotDirInput";
            this.screenshotDirInput.Size = new System.Drawing.Size(445, 23);
            this.screenshotDirInput.TabIndex = 0;
            this.screenshotDirInput.Text = "directory";
            this.screenshotDirInput.TextChanged += new System.EventHandler(this.DirectoryInputTextChanged);
            // 
            // screenshotDirSelectButton
            // 
            this.screenshotDirSelectButton.Location = new System.Drawing.Point(565, 11);
            this.screenshotDirSelectButton.Margin = new System.Windows.Forms.Padding(0);
            this.screenshotDirSelectButton.Name = "screenshotDirSelectButton";
            this.screenshotDirSelectButton.Size = new System.Drawing.Size(93, 25);
            this.screenshotDirSelectButton.TabIndex = 0;
            this.screenshotDirSelectButton.Text = "Open";
            this.screenshotDirSelectButton.UseVisualStyleBackColor = true;
            this.screenshotDirSelectButton.Click += new System.EventHandler(this.OnDirectorySelectButtonClicked);
            // 
            // backupDirInput
            // 
            this.backupDirInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.backupDirInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.backupDirInput.Location = new System.Drawing.Point(116, 40);
            this.backupDirInput.Margin = new System.Windows.Forms.Padding(0);
            this.backupDirInput.Name = "backupDirInput";
            this.backupDirInput.Size = new System.Drawing.Size(445, 23);
            this.backupDirInput.TabIndex = 0;
            this.backupDirInput.Text = "directory";
            this.backupDirInput.TextChanged += new System.EventHandler(this.DirectoryInputTextChanged);
            // 
            // backupDirSelectButton
            // 
            this.backupDirSelectButton.Location = new System.Drawing.Point(565, 39);
            this.backupDirSelectButton.Margin = new System.Windows.Forms.Padding(0);
            this.backupDirSelectButton.Name = "backupDirSelectButton";
            this.backupDirSelectButton.Size = new System.Drawing.Size(93, 25);
            this.backupDirSelectButton.TabIndex = 0;
            this.backupDirSelectButton.Text = "Open";
            this.backupDirSelectButton.UseVisualStyleBackColor = true;
            this.backupDirSelectButton.Click += new System.EventHandler(this.OnDirectorySelectButtonClicked);
            // 
            // canDeleteCheckBox
            // 
            this.canDeleteCheckBox.AutoSize = true;
            this.canDeleteCheckBox.Location = new System.Drawing.Point(12, 279);
            this.canDeleteCheckBox.Name = "canDeleteCheckBox";
            this.canDeleteCheckBox.Size = new System.Drawing.Size(143, 19);
            this.canDeleteCheckBox.TabIndex = 0;
            this.canDeleteCheckBox.Text = "Delete completed files";
            this.canDeleteCheckBox.UseVisualStyleBackColor = true;
            this.canDeleteCheckBox.CheckedChanged += new System.EventHandler(this.OnCanDeleteCheckBoxCheckedChanged);
            // 
            // screenshotsFindButton
            // 
            this.screenshotsFindButton.Enabled = false;
            this.screenshotsFindButton.Location = new System.Drawing.Point(420, 248);
            this.screenshotsFindButton.Name = "screenshotsFindButton";
            this.screenshotsFindButton.Size = new System.Drawing.Size(116, 27);
            this.screenshotsFindButton.TabIndex = 0;
            this.screenshotsFindButton.Text = "Find";
            this.screenshotsFindButton.UseVisualStyleBackColor = true;
            this.screenshotsFindButton.Click += new System.EventHandler(this.OnFindButtonClicked);
            // 
            // openBackupDirButton
            // 
            this.openBackupDirButton.Location = new System.Drawing.Point(419, 318);
            this.openBackupDirButton.Name = "openBackupDirButton";
            this.openBackupDirButton.Size = new System.Drawing.Size(238, 27);
            this.openBackupDirButton.TabIndex = 0;
            this.openBackupDirButton.Text = "Open Backup Folder";
            this.openBackupDirButton.UseVisualStyleBackColor = true;
            this.openBackupDirButton.Click += new System.EventHandler(this.OnOpenBackupDirButtonClicked);
            // 
            // backupButton
            // 
            this.backupButton.Enabled = false;
            this.backupButton.Location = new System.Drawing.Point(542, 248);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(116, 27);
            this.backupButton.TabIndex = 0;
            this.backupButton.Text = "Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.OnBackupButtonClicked);
            // 
            // backupLog
            // 
            this.backupLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backupLog.FormattingEnabled = true;
            this.backupLog.ItemHeight = 15;
            this.backupLog.Location = new System.Drawing.Point(12, 74);
            this.backupLog.Name = "backupLog";
            this.backupLog.ScrollAlwaysVisible = true;
            this.backupLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.backupLog.Size = new System.Drawing.Size(645, 167);
            this.backupLog.TabIndex = 0;
            this.backupLog.TabStop = false;
            // 
            // backupProgressBar
            // 
            this.backupProgressBar.Location = new System.Drawing.Point(12, 249);
            this.backupProgressBar.Name = "backupProgressBar";
            this.backupProgressBar.Size = new System.Drawing.Size(401, 25);
            this.backupProgressBar.Step = 1;
            this.backupProgressBar.TabIndex = 0;
            // 
            // dirSelectDialog
            // 
            this.dirSelectDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // saveLogDialog
            // 
            this.saveLogDialog.Filter = "Log file (*.log)|*.log";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 405);
            this.Controls.Add(panel1);
            this.Controls.Add(mainToolStrip);
            this.Controls.Add(mainStatusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Maple Screenshot Backup";
            this.Load += new System.EventHandler(this.OnLoadForm);
            mainToolStrip.ResumeLayout(false);
            mainToolStrip.PerformLayout();
            mainStatusStrip.ResumeLayout(false);
            mainStatusStrip.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton exportLogButton;
        private System.Windows.Forms.TextBox screenshotDirInput;
        private System.Windows.Forms.Button screenshotDirSelectButton;
        private System.Windows.Forms.TextBox backupDirInput;
        private System.Windows.Forms.Button backupDirSelectButton;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.ProgressBar backupProgressBar;
        private System.Windows.Forms.Button screenshotsFindButton;
        private System.Windows.Forms.ListBox backupLog;
        private System.Windows.Forms.CheckBox canDeleteCheckBox;
        private System.Windows.Forms.ToolStripButton newReleaseButton;
        private System.Windows.Forms.ToolStripStatusLabel versionLabel;
        private System.Windows.Forms.ToolStripStatusLabel githubLink;
        private System.Windows.Forms.Button openBackupDirButton;
        private System.Windows.Forms.FolderBrowserDialog dirSelectDialog;
        private System.Windows.Forms.SaveFileDialog saveLogDialog;
    }
}
