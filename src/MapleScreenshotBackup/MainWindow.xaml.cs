using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MapleScreenshotBackup.WPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Log _log;
    private Backup? _backupProcess;

    public MainWindow()
    {
        InitializeComponent();

        VersionTextBlock.Text = $"Maple Screenshot Backup v{ProductVersion}";
        NewReleaseButton.Visibility = Visibility.Collapsed;

        BackupButton.IsEnabled = false;
        ScreenshotsFindButton.IsEnabled = false;
        OpenBackupDirButton.IsEnabled = false;

        _log = new Log(BackupLogBox);

        Dispatcher.InvokeAsync(async () =>
        {
            await Config.LoadAsync();
            ScreenshotDirTextBox.Text = Config.Item.ScreenshotDirectory;
            BackupDirTextBox.Text = Config.Item.BackupDirectory;

            switch (Config.Item.FinishedScreenshot)
            {
                case FinishedScreenshotOption.SendToRecycleBin:
                    SendToRecycleBinOption.IsChecked = true;
                    break;
                case FinishedScreenshotOption.DeletePermanently:
                    DeletePermanentlyOption.IsChecked = true;
                    break;
                case FinishedScreenshotOption.DoNotDelete:
                    DoNotDeleteOption.IsChecked = true;
                    break;
            }

            _log.WriteLine("Checking program versions...", true);
            await CheckVersion();
        });
    }

    private static string ProductVersion
    {
        get => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.1";
    }

    private void GitHubButton_Click(object sender, RoutedEventArgs e)
    {
        OpenHyperLink("https://github.com/pid011/maple-screenshot-backup");
    }

    private void OpenHyperLink(string url)
    {
        try
        {
            _log.WriteLine($"Open url [{url}]", true);
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            _log.WriteLine(ex);
        }
    }

    private async Task CheckVersion()
    {
        var version = ProductVersion;
        var update = await GitHubRelease.CompareVersionAsync(version);
        if (update is not null)
        {
            var (compare, release) = update.Value;

            if (!compare)
            {
                _log.WriteLine($"New release found! [v{version} -> {release.TagName}]");

                NewReleaseButton.Content = release.TagName;
                NewReleaseButton.ToolTip = release.Url;
                NewReleaseButton.Visibility = Visibility.Visible;
                NewReleaseButton.Click += (sender, e) =>
                {
                    OpenHyperLink(release.Url);
                };
            }
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

    private void ScreenshotDirOpenButton_Click(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog();
        if (folderDialog.ShowDialog() == true)
        {
            var folderName = folderDialog.FolderName;
            ScreenshotDirTextBox.Text = folderName;
        }
    }

    private void BackupDirOpenButton_Click(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog();
        if (folderDialog.ShowDialog() == true)
        {
            var folderName = folderDialog.FolderName;
            BackupDirTextBox.Text = folderName;
        }
    }

    private void ScreenshotDirTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox target)
        {
            return;
        }

        Config.Item.ScreenshotDirectory = target.Text;

        var dirSameCheck = Config.Item.ScreenshotDirectory != Config.Item.BackupDirectory;
        var screenshotDirCheck = CheckDirectoryPath(Config.Item.ScreenshotDirectory);
        var backupDirCheck = CheckDirectoryPath(Config.Item.BackupDirectory);

        OpenBackupDirButton.IsEnabled = backupDirCheck;

        ScreenshotsFindButton.IsEnabled = dirSameCheck && screenshotDirCheck && backupDirCheck;
        BackupButton.IsEnabled = false;
    }

    private void BackupDirTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox target)
        {
            return;
        }

        Config.Item.BackupDirectory = target.Text;

        var dirSameCheck = Config.Item.ScreenshotDirectory != Config.Item.BackupDirectory;
        var screenshotDirCheck = CheckDirectoryPath(Config.Item.ScreenshotDirectory);
        var backupDirCheck = CheckDirectoryPath(Config.Item.BackupDirectory);

        OpenBackupDirButton.IsEnabled = backupDirCheck;

        ScreenshotsFindButton.IsEnabled = dirSameCheck && screenshotDirCheck && backupDirCheck;
        BackupButton.IsEnabled = false;
    }

    private async void ScreenshotsFindButton_Click(object sender, RoutedEventArgs e)
    {
        ScreenshotsFindButton.IsEnabled = false;

        try
        {
            await Config.SaveAsync();

            _log.WriteLine($"Screenshot directory: {Config.Item.ScreenshotDirectory}");
            _log.WriteLine($"Backup directory: {Config.Item.BackupDirectory}");

            _backupProcess = new Backup(Config.Item.ScreenshotDirectory, Config.Item.BackupDirectory);
            _log.WriteLine("Finding...");

            await _backupProcess.FindScreenshotsAsync(BackupProgressBar);

            var count = _backupProcess.ScreenshotsPathCache.Count;
            _log.WriteLine($"Screenshots count: {count}");

            if (count != 0)
            {
                _backupProcess.ScreenshotsPathCache.ForEach(s => _log.WriteLine(s, true));
                BackupButton.IsEnabled = true;
            }
            else
            {
                _log.WriteLine("There are no screenshots to backup.");
                BackupButton.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            _log.WriteLine(ex);
            BackupButton.IsEnabled = false;
        }
        finally
        {
            ScreenshotsFindButton.IsEnabled = true;
            _log.WriteLine();
        }
    }

    private async void BackupButton_Click(object sender, RoutedEventArgs e)
    {
        BackupButton.IsEnabled = false;

        if (_backupProcess is null)
        {
            return;
        }

        try
        {
            await Config.SaveAsync();

            _log.WriteLine($"Finished screenshot: {Config.Item.FinishedScreenshot}");

            _log.WriteLine("Backup in progress...");
            var result = await _backupProcess.StartBackupAsync(BackupProgressBar, Config.Item.FinishedScreenshot);
            _log.WriteLine("Done.");

            _log.WriteLine($"Faild count: {result.Faild.Count}");
            result.Faild.ForEach(s => _log.WriteLine(s, true));
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

    private void OpenBackupDirButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var path = Config.Item.BackupDirectory;
            if (CheckDirectoryPath(path))
            {
                _log.WriteLine("Open backup directory.", true);
                Process.Start("explorer.exe", path);
            }
        }
        catch (Exception ex)
        {
            _log.WriteLine(ex);
        }
    }

    private async void ExportLogButton_Click(object sender, RoutedEventArgs e)
    {
        ExportLogButton.IsEnabled = false;

        try
        {
            var folderDialog = new SaveFileDialog
            {
                Filter = "log files (*.log)|*.log|All files (*.*)|*.*", FileName = "MapleScreenshotBackup.log"
            };
            if (folderDialog.ShowDialog() == true)
            {
                var fileName = folderDialog.FileName;

                _log.WriteLine("Exporting log...", true);
                await _log.ExportLogAsync(fileName);
                Process.Start("explorer.exe", $"/select,{fileName}");
            }
        }
        catch (Exception ex)
        {
            _log.WriteLine(ex);
        }
        finally
        {
            ExportLogButton.IsEnabled = true;
        }
    }
}
