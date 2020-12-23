
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
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.ToolStrip mainToolStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TabControl mainTabControl;
            System.Windows.Forms.TabPage backupPage;
            System.Windows.Forms.TableLayoutPanel backupDirInputPanel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.TableLayoutPanel mapleDirInputPanel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.TabPage viewerPage;
            System.Windows.Forms.StatusStrip mainStatusStrip;
            System.Windows.Forms.Panel panel1;
            this.screenshotsTreeView = new System.Windows.Forms.TreeView();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backupProgressBar = new System.Windows.Forms.ProgressBar();
            this.screenshotsFindButton = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.backupStatus = new System.Windows.Forms.Label();
            this.backupDirInput = new System.Windows.Forms.TextBox();
            this.backupDirSelectButton = new System.Windows.Forms.Button();
            this.mapleDirInput = new System.Windows.Forms.TextBox();
            this.mapleDirSelectButton = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            mainToolStrip = new System.Windows.Forms.ToolStrip();
            mainTabControl = new System.Windows.Forms.TabControl();
            backupPage = new System.Windows.Forms.TabPage();
            backupDirInputPanel = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            mapleDirInputPanel = new System.Windows.Forms.TableLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            viewerPage = new System.Windows.Forms.TabPage();
            mainStatusStrip = new System.Windows.Forms.StatusStrip();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            mainToolStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            backupPage.SuspendLayout();
            backupDirInputPanel.SuspendLayout();
            mapleDirInputPanel.SuspendLayout();
            viewerPage.SuspendLayout();
            panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.screenshotsTreeView);
            splitContainer1.Size = new System.Drawing.Size(770, 480);
            splitContainer1.SplitterDistance = 223;
            splitContainer1.TabIndex = 0;
            // 
            // screenshotsTreeView
            // 
            this.screenshotsTreeView.BackColor = System.Drawing.SystemColors.Control;
            this.screenshotsTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenshotsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenshotsTreeView.Location = new System.Drawing.Point(0, 0);
            this.screenshotsTreeView.Margin = new System.Windows.Forms.Padding(0);
            this.screenshotsTreeView.Name = "screenshotsTreeView";
            this.screenshotsTreeView.Size = new System.Drawing.Size(223, 480);
            this.screenshotsTreeView.TabIndex = 0;
            // 
            // mainToolStrip
            // 
            mainToolStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton});
            mainToolStrip.Location = new System.Drawing.Point(0, 0);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Size = new System.Drawing.Size(784, 25);
            mainToolStrip.TabIndex = 0;
            mainToolStrip.Text = "mainToolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(backupPage);
            mainTabControl.Controls.Add(viewerPage);
            mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTabControl.HotTrack = true;
            mainTabControl.Location = new System.Drawing.Point(0, 0);
            mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new System.Drawing.Size(784, 514);
            mainTabControl.TabIndex = 1;
            // 
            // backupPage
            // 
            backupPage.Controls.Add(this.backupProgressBar);
            backupPage.Controls.Add(this.screenshotsFindButton);
            backupPage.Controls.Add(this.backupButton);
            backupPage.Controls.Add(this.backupStatus);
            backupPage.Controls.Add(backupDirInputPanel);
            backupPage.Controls.Add(mapleDirInputPanel);
            backupPage.Location = new System.Drawing.Point(4, 24);
            backupPage.Name = "backupPage";
            backupPage.Padding = new System.Windows.Forms.Padding(3);
            backupPage.Size = new System.Drawing.Size(776, 486);
            backupPage.TabIndex = 0;
            backupPage.Text = "Backup";
            backupPage.UseVisualStyleBackColor = true;
            // 
            // backupProgressBar
            // 
            this.backupProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.backupProgressBar.Location = new System.Drawing.Point(60, 285);
            this.backupProgressBar.Name = "backupProgressBar";
            this.backupProgressBar.Size = new System.Drawing.Size(401, 25);
            this.backupProgressBar.TabIndex = 3;
            // 
            // screenshotsFindButton
            // 
            this.screenshotsFindButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.screenshotsFindButton.Enabled = false;
            this.screenshotsFindButton.Location = new System.Drawing.Point(467, 284);
            this.screenshotsFindButton.Name = "screenshotsFindButton";
            this.screenshotsFindButton.Size = new System.Drawing.Size(116, 27);
            this.screenshotsFindButton.TabIndex = 2;
            this.screenshotsFindButton.Text = "Find";
            this.screenshotsFindButton.UseVisualStyleBackColor = true;
            this.screenshotsFindButton.Click += new System.EventHandler(this.OnFindButtonClicked);
            // 
            // backupButton
            // 
            this.backupButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.backupButton.Enabled = false;
            this.backupButton.Location = new System.Drawing.Point(589, 284);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(116, 27);
            this.backupButton.TabIndex = 2;
            this.backupButton.Text = "Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.OnBackupButtonClicked);
            // 
            // backupStatus
            // 
            this.backupStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.backupStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backupStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.backupStatus.Location = new System.Drawing.Point(60, 123);
            this.backupStatus.Name = "backupStatus";
            this.backupStatus.Size = new System.Drawing.Size(645, 158);
            this.backupStatus.TabIndex = 1;
            this.backupStatus.Text = "asdfsadf\r\nsadf\r\nasdfasd\r\nfsdafasdfasdf";
            // 
            // backupDirInputPanel
            // 
            backupDirInputPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            backupDirInputPanel.ColumnCount = 3;
            backupDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            backupDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            backupDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            backupDirInputPanel.Controls.Add(label1, 0, 0);
            backupDirInputPanel.Controls.Add(this.backupDirInput, 1, 0);
            backupDirInputPanel.Controls.Add(this.backupDirSelectButton, 2, 0);
            backupDirInputPanel.Location = new System.Drawing.Point(60, 51);
            backupDirInputPanel.Name = "backupDirInputPanel";
            backupDirInputPanel.RowCount = 1;
            backupDirInputPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            backupDirInputPanel.Size = new System.Drawing.Size(645, 27);
            backupDirInputPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Margin = new System.Windows.Forms.Padding(0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(129, 27);
            label1.TabIndex = 0;
            label1.Text = "Backup Directory";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // backupDirInput
            // 
            this.backupDirInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.backupDirInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.backupDirInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupDirInput.Location = new System.Drawing.Point(132, 3);
            this.backupDirInput.Name = "backupDirInput";
            this.backupDirInput.Size = new System.Drawing.Size(413, 23);
            this.backupDirInput.TabIndex = 1;
            this.backupDirInput.TextChanged += new System.EventHandler(this.DirectoryInputTextChanged);
            // 
            // backupDirSelectButton
            // 
            this.backupDirSelectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupDirSelectButton.Location = new System.Drawing.Point(550, 2);
            this.backupDirSelectButton.Margin = new System.Windows.Forms.Padding(2);
            this.backupDirSelectButton.Name = "backupDirSelectButton";
            this.backupDirSelectButton.Size = new System.Drawing.Size(93, 23);
            this.backupDirSelectButton.TabIndex = 2;
            this.backupDirSelectButton.Text = "Open";
            this.backupDirSelectButton.UseVisualStyleBackColor = true;
            this.backupDirSelectButton.Click += new System.EventHandler(this.OnDirectorySelectButtonClicked);
            // 
            // mapleDirInputPanel
            // 
            mapleDirInputPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            mapleDirInputPanel.ColumnCount = 3;
            mapleDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            mapleDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            mapleDirInputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            mapleDirInputPanel.Controls.Add(label2, 0, 0);
            mapleDirInputPanel.Controls.Add(this.mapleDirInput, 1, 0);
            mapleDirInputPanel.Controls.Add(this.mapleDirSelectButton, 2, 0);
            mapleDirInputPanel.Location = new System.Drawing.Point(60, 18);
            mapleDirInputPanel.Name = "mapleDirInputPanel";
            mapleDirInputPanel.RowCount = 1;
            mapleDirInputPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mapleDirInputPanel.Size = new System.Drawing.Size(645, 27);
            mapleDirInputPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Margin = new System.Windows.Forms.Padding(0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(129, 27);
            label2.TabIndex = 0;
            label2.Text = "MapleStory Directory";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapleDirInput
            // 
            this.mapleDirInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.mapleDirInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.mapleDirInput.BackColor = System.Drawing.SystemColors.Window;
            this.mapleDirInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapleDirInput.Location = new System.Drawing.Point(132, 3);
            this.mapleDirInput.Name = "mapleDirInput";
            this.mapleDirInput.Size = new System.Drawing.Size(413, 23);
            this.mapleDirInput.TabIndex = 1;
            this.mapleDirInput.TextChanged += new System.EventHandler(this.DirectoryInputTextChanged);
            // 
            // mapleDirSelectButton
            // 
            this.mapleDirSelectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapleDirSelectButton.Location = new System.Drawing.Point(550, 2);
            this.mapleDirSelectButton.Margin = new System.Windows.Forms.Padding(2);
            this.mapleDirSelectButton.Name = "mapleDirSelectButton";
            this.mapleDirSelectButton.Size = new System.Drawing.Size(93, 23);
            this.mapleDirSelectButton.TabIndex = 2;
            this.mapleDirSelectButton.Text = "Open";
            this.mapleDirSelectButton.UseVisualStyleBackColor = true;
            this.mapleDirSelectButton.Click += new System.EventHandler(this.OnDirectorySelectButtonClicked);
            // 
            // viewerPage
            // 
            viewerPage.Controls.Add(splitContainer1);
            viewerPage.Location = new System.Drawing.Point(4, 24);
            viewerPage.Name = "viewerPage";
            viewerPage.Padding = new System.Windows.Forms.Padding(3);
            viewerPage.Size = new System.Drawing.Size(776, 486);
            viewerPage.TabIndex = 1;
            viewerPage.Text = "Viewer";
            viewerPage.UseVisualStyleBackColor = true;
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            mainStatusStrip.Location = new System.Drawing.Point(0, 539);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            mainStatusStrip.Size = new System.Drawing.Size(784, 22);
            mainStatusStrip.TabIndex = 2;
            mainStatusStrip.Text = "statusStrip1";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(mainTabControl);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 25);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(784, 514);
            panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(panel1);
            this.Controls.Add(mainStatusStrip);
            this.Controls.Add(mainToolStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Maple Screenshot Backup";
            this.Load += new System.EventHandler(this.OnLoadForm);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            mainToolStrip.ResumeLayout(false);
            mainToolStrip.PerformLayout();
            mainTabControl.ResumeLayout(false);
            backupPage.ResumeLayout(false);
            backupDirInputPanel.ResumeLayout(false);
            backupDirInputPanel.PerformLayout();
            mapleDirInputPanel.ResumeLayout(false);
            mapleDirInputPanel.PerformLayout();
            viewerPage.ResumeLayout(false);
            panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.TextBox mapleDirInput;
        private System.Windows.Forms.Button mapleDirSelectButton;
        private System.Windows.Forms.TextBox backupDirInput;
        private System.Windows.Forms.Button backupDirSelectButton;
        private System.Windows.Forms.TreeView screenshotsTreeView;
        private System.Windows.Forms.Label backupStatus;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.ProgressBar backupProgressBar;
        private System.Windows.Forms.Button screenshotsFindButton;
    }
}
