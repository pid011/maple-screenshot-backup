using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MapleScreenshotBackup.Forms
{
    public partial class MainForm : Form
    {
        private readonly Log _log;
        private ConfigItem _config;
        private Backup _backupProcess;

        public MainForm()
        {
            InitializeComponent();

            versionLabel.Text = $"Maple Screenshot Backup v{ProductVersion}";
            newReleaseButton.Visible = false;
            githubLink.Click += (sender, e) =>
            {
                OpenHyperLink("https://github.com/pid011/maple-screenshot-backup");
            };

            backupButton.Enabled = false;
            _log = new Log(backupLog);
        }

        private async void OnLoadForm(object sender, EventArgs e)
        {
            _log.WriteLine("Loading config...", hide: true);
            _config = await Config.LoadConfig();
            if (_config == null)
            {
                _log.WriteLine("Creating new config...", hide: true);
                _config = new ConfigItem
                {
                    ScreenshotFolder = string.Empty,
                    BackupFolder = string.Empty,
                    CanDelete = false
                };
                await Config.WriteConfig(_config);
            }
            _log.WriteLine("Config loaded.", hide: true);

            screenshotDirInput.Text = _config.ScreenshotFolder;
            backupDirInput.Text = _config.BackupFolder;

            canDeleteCheckBox.Checked = _config.CanDelete;

            _log.WriteLine("Checking program versions...", hide: true);
            var update = await GitHubRelease.CompareVersionAsync(Application.ProductVersion);
            if (update is not null)
            {
                var (compare, url) = update.Value;

                if (!compare)
                {
                    _log.WriteLine("New version founds!");
                    newReleaseButton.Visible = true;
                    newReleaseButton.Click += (sender, e) =>
                    {
                        OpenHyperLink(url);
                    };
                }
            }
        }
        private void OnDirectorySelectButtonClicked(object sender, EventArgs e)
        {
            if (sender is not Button target)
            {
                return;
            }

            if (dirSelectDialog.ShowDialog() == DialogResult.OK)
            {
                var path = dirSelectDialog.SelectedPath;

                if (dirSelectDialog.SelectedPath == string.Empty)
                {
                    return;
                }

                if (target.Name == screenshotDirSelectButton.Name)
                {
                    screenshotDirInput.Text = path;
                }

                if (target.Name == backupDirSelectButton.Name)
                {
                    backupDirInput.Text = path;
                }
            }
        }

        private void DirectoryInputTextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox target)
            {
                return;
            }

            if (target.Name == screenshotDirInput.Name)
            {
                _config.ScreenshotFolder = target.Text;
            }

            if (target.Name == backupDirInput.Name)
            {
                _config.BackupFolder = target.Text;
            }

            var screenshotFolderCheck = CheckDirectoryPath(_config.ScreenshotFolder);
            var backupFolderCheck = CheckDirectoryPath(_config.BackupFolder);

            openBackupFolderButton.Enabled = backupFolderCheck;
            screenshotsFindButton.Enabled = screenshotFolderCheck && backupFolderCheck;
        }

        private async void OnFindButtonClicked(object sender, EventArgs e)
        {
            screenshotsFindButton.Enabled = false;
            try
            {
                _config.ScreenshotFolder = screenshotDirInput.Text;
                _config.BackupFolder = backupDirInput.Text;
                _config.CanDelete = canDeleteCheckBox.Checked;

                backupProgressBar.Style = ProgressBarStyle.Marquee;
                await Config.WriteConfig(_config);
                backupProgressBar.Style = ProgressBarStyle.Blocks;

                _log.WriteLine($"Screenshot folder: {_config.ScreenshotFolder}");
                _log.WriteLine($"Backup folder: {_config.BackupFolder}");

                _backupProcess = new Backup(_config);
                _log.WriteLine("Finding...");

                await _backupProcess.FindScreenshotsAsync(backupProgressBar);

                var count = _backupProcess.ScreenshotsPathCache.Count;
                _log.WriteLine($"Screenshots count: {count}");

                if (count != 0)
                {
                    _backupProcess.ScreenshotsPathCache.ForEach(s => _log.WriteLine(s, hide: true));
                    backupButton.Enabled = true;
                }
                else
                {
                    _log.WriteLine("There are no screenshots to backup.");
                    backupButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
                backupButton.Enabled = false;
            }
            finally
            {
                screenshotsFindButton.Enabled = true;
                _log.WriteLine();
            }
        }

        private async void OnBackupButtonClicked(object sender, EventArgs e)
        {
            backupButton.Enabled = false;

            try
            {
                backupProgressBar.Style = ProgressBarStyle.Marquee;
                _config.CanDelete = canDeleteCheckBox.Checked;
                await Config.WriteConfig(_config);
                backupProgressBar.Style = ProgressBarStyle.Blocks;

                _log.WriteLine($"Delete completed files: {canDeleteCheckBox.Checked}");

                _log.WriteLine("Backup in progress...");
                var result = await _backupProcess.StartBackupAsync(backupProgressBar, canDeleteCheckBox.Checked);
                _log.WriteLine("Done.");

                _log.WriteLine($"Faild count: {result.Faild.Count}");
                result.Faild.ForEach(s => _log.WriteLine(s, hide: true));

                _log.WriteLine($"Skip count: {result.Skip.Count}");
                result.Skip.ForEach(s => _log.WriteLine(s, hide: true));
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
            }
            finally
            {
                _log.WriteLine();
            }
        }

        private async void OnExportLogButtonClicked(object sender, EventArgs e)
        {
            exportLogButton.Enabled = false;
            try
            {
                if (saveLogDialog.ShowDialog() == DialogResult.OK)
                {
                    _log.WriteLine("Exporting log...", hide: true);
                    await _log.ExportLogAsync(saveLogDialog.FileName);
                    MessageBox.Show("Done.");
                }
            }
            finally
            {
                exportLogButton.Enabled = true;
            }
        }

        private void OnOpenBackupFolderButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (CheckDirectoryPath(_config.BackupFolder))
                {
                    _log.WriteLine("Open backup directory.", hide: true);
                    Process.Start(new ProcessStartInfo("explorer.exe", _config.BackupFolder) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
            }
        }

        private void OpenHyperLink(string url)
        {
            try
            {
                _log.WriteLine($"Open url [{url}]", hide: true);
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
            }
        }

        private static bool CheckDirectoryPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            try
            {
                var fullPath = Path.GetFullPath(path);
                return Directory.Exists(fullPath);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
