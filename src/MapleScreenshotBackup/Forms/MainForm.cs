using System;
using System.IO;
using System.Windows.Forms;

namespace MapleScreenshotBackup.Forms
{
    public partial class MainForm : Form
    {
        private BackupDirectories _directoriesConfig;

        public MainForm()
        {
            InitializeComponent();
            backupButton.Enabled = false;
        }

        private async void OnLoadForm(object sender, EventArgs e)
        {
            _directoriesConfig = await Config.LoadConfig();
            if (_directoriesConfig == null)
            {
                _directoriesConfig = new BackupDirectories
                {
                    BackupDirectory = string.Empty,
                    MapleDirectory = string.Empty
                };
                await Config.WriteConfig(_directoriesConfig);
            }

            mapleDirInput.Text = _directoriesConfig.MapleDirectory;
            backupDirInput.Text = _directoriesConfig.BackupDirectory;
        }

        private void OnDirectorySelectButtonClicked(object sender, EventArgs e)
        {
            if (sender is not Button target)
            {
                return;
            }

            using var dialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            var ret = dialog.ShowDialog();
            if (ret == DialogResult.OK)
            {
                var path = dialog.SelectedPath;

                if (dialog.SelectedPath == string.Empty)
                {
                    return;
                }

                if (target.Name == mapleDirSelectButton.Name)
                {
                    mapleDirInput.Text = path;
                }

                if (target.Name == backupDirSelectButton.Name)
                {
                    backupDirInput.Text = path;
                }

                InputValueCheck();
            }
        }

        private void DirectoryInputTextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox target || string.IsNullOrWhiteSpace(target.Text))
            {
                return;
            }

            InputValueCheck();
        }

        private bool InputValueCheck()
        {
            var ret = Check(mapleDirInput) && Check(backupDirInput);

            screenshotsFindButton.Enabled = ret;
            return ret;

            static bool Check(TextBox target)
            {
                if (string.IsNullOrWhiteSpace(target.Text))
                {
                    return false;
                }

                try
                {
                    var fullPath = Path.GetFullPath(target.Text);
                    return Directory.Exists(fullPath);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private async void OnFindButtonClicked(object sender, EventArgs e)
        {
            screenshotsFindButton.Enabled = false;
            try
            {
                _directoriesConfig = _directoriesConfig with
                {
                    MapleDirectory = mapleDirInput.Text,
                    BackupDirectory = backupDirInput.Text
                };

                backupProgressBar.Style = ProgressBarStyle.Marquee;
                var ret = await Config.WriteConfig(_directoriesConfig);
                if (!ret)
                {
                    return;
                }
                //backupProgressBar.Style = ProgressBarStyle.Blocks;

                backupButton.Enabled = true;
            }
            finally
            {
                screenshotsFindButton.Enabled = true;
            }
        }

        private void OnBackupButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
