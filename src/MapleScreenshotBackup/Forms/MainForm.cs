using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MapleScreenshotBackup.Properties;

namespace MapleScreenshotBackup.Forms
{
    public partial class MainForm : Form
    {
        private readonly Log _log;
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

            _log = new Log(backupLog);

            backupButton.Enabled = false;
            screenshotDirInput.Text = Settings.Default.ScreenshotDir;
            backupDirInput.Text = Settings.Default.BackupDir;
            canDeleteCheckBox.Checked = Settings.Default.CanDelete;
        }

        private async void OnLoadForm(object sender, EventArgs e)
        {
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
                Settings.Default.ScreenshotDir = target.Text;
            }

            if (target.Name == backupDirInput.Name)
            {
                Settings.Default.BackupDir = target.Text;
            }

            var screenshotDirCheck = CheckDirectoryPath(Settings.Default.ScreenshotDir);
            var backupDirCheck = CheckDirectoryPath(Settings.Default.BackupDir);

            openBackupDirButton.Enabled = backupDirCheck;
            screenshotsFindButton.Enabled = screenshotDirCheck && backupDirCheck;
            backupButton.Enabled = false;
        }

        private async void OnFindButtonClicked(object sender, EventArgs e)
        {
            screenshotsFindButton.Enabled = false;

            try
            {
                Settings.Default.Save();

                _log.WriteLine($"Screenshot directory: {Settings.Default.ScreenshotDir}");
                _log.WriteLine($"Backup directory: {Settings.Default.BackupDir}");

                _backupProcess = new Backup(Settings.Default.ScreenshotDir, Settings.Default.BackupDir);
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

                Settings.Default.CanDelete = canDeleteCheckBox.Checked;
                Settings.Default.Save();

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

                    Process.Start("explorer.exe", $"/select,{saveLogDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
            }
            finally
            {
                exportLogButton.Enabled = true;
            }
        }

        private void OnOpenBackupDirButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var path = Settings.Default.BackupDir;
                if (CheckDirectoryPath(path))
                {
                    _log.WriteLine("Open backup directory.", hide: true);
                    Process.Start("explorer.exe", path);
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine(ex);
            }
        }

        private void OnCanDeleteCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.CanDelete = canDeleteCheckBox.Checked;
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
