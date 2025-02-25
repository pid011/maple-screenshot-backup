using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

namespace MapleScreenshotBackup;

public partial class Backup
{
    private static readonly AsyncLock s_lock = new();
    private readonly string _backupDir;

    private readonly string[] _extensions = [".png", ".jpg"];
    private readonly string _screenshotDir;

    public Backup(string screenshotDir, string backupDir)
    {
        if (!Directory.Exists(screenshotDir))
        {
            throw new ArgumentException("Wrong directory.", nameof(screenshotDir));
        }

        if (!Directory.Exists(backupDir))
        {
            throw new ArgumentException("Wrong directory.", nameof(backupDir));
        }

        _screenshotDir = screenshotDir;
        _backupDir = backupDir;
    }

    public List<(string source, string dest)> ScreenshotsPathCache { get; private set; } = [];

    public async Task FindScreenshotsAsync(ProgressBar progress)
    {
        using var disposer = await s_lock.LockAsync();
        progress.IsIndeterminate = true;

        ScreenshotsPathCache = await Task.Run(() =>
        {
            var files = new List<string>();
            foreach (var ext in _extensions)
            {
                files.AddRange(Directory.GetFiles(_screenshotDir, "*" + ext, SearchOption.TopDirectoryOnly));
            }

            var result = new List<(string source, string dest)>(files.Count);

            foreach (var source in files)
            {
                var filename = Path.GetFileName(source);
                var position = GetBackupPathFromFileName(filename);
                if (position == null)
                {
                    continue;
                }

                var screenshotDir = Path.Combine(_backupDir, position);
                var dest = Path.Combine(screenshotDir, filename);

                result.Add((source, dest));
            }

            result.TrimExcess();

            return result;
        });

        progress.IsIndeterminate = false;

        static string? GetBackupPathFromFileName(string filename)
        {
            var matches = BackupDateRegex().Matches(filename);
            if (matches.Count != 2)
            {
                return null;
            }

            var dateStr = matches[0].Value;
            var year = string.Concat("20", dateStr.AsSpan(0, 2));
            var month = dateStr.Substring(2, 2);
            var day = dateStr.Substring(4, 2);

            return Path.Combine(year, month, day);
        }
    }

    public async Task<Result> StartBackupAsync(ProgressBar progress, FinishedScreenshotOption option)
    {
        using var disposer = await s_lock.LockAsync();

        var defaultProgress = new { progress.Maximum };

        progress.IsIndeterminate = false;
        progress.Value = 0;
        progress.Maximum = ScreenshotsPathCache.Count;

        try
        {
            var result = await Task.Run(() => ProcessBackup(progress, option));
            return result;
        }
        finally
        {
            progress.Value = 0;
            progress.Maximum = defaultProgress.Maximum;
        }
    }

    private Result ProcessBackup(ProgressBar progress, FinishedScreenshotOption option)
    {
        var faild = new List<BackupException>();

        foreach (var (source, dest) in ScreenshotsPathCache)
        {
            try
            {
                if (source == dest)
                {
                    faild.Add(new BackupException(source, "Source file path and Backup file path is same."));
                    continue;
                }

                var screenshotDir = Path.GetDirectoryName(dest);
                Directory.CreateDirectory(screenshotDir);

                var sourceFile = new FileInfo(source);
                if (File.Exists(dest))
                {
                    var destFile = new FileInfo(dest);

                    // Do not copy if file name is already exist but file size is diffrent
                    // (Not same file)
                    if (sourceFile.Length != destFile.Length)
                    {
                        faild.Add(new BackupException(source,
                            $"File name [{dest}] is already exist and It is not same file."));
                        continue;
                    }
                }

                // Overwriting file
                sourceFile.CopyTo(dest, true);

                switch (option)
                {
                    case FinishedScreenshotOption.SendToRecycleBin:
                        FileSystem.DeleteFile(
                            source,
                            UIOption.OnlyErrorDialogs,
                            RecycleOption.SendToRecycleBin);
                        break;

                    case FinishedScreenshotOption.DeletePermanently:
                        sourceFile.Delete();
                        break;

                    case FinishedScreenshotOption.DoNotDelete:
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                faild.Add(new BackupException(source, $"Faild to backup screenshot [{source}]", e));
            }
            finally
            {
                progress.Dispatcher.BeginInvoke(new Action(() => progress.Value++));
            }
        }

        return new Result(faild.Count == 0, faild);
    }

    [GeneratedRegex("([0-9]{6})")]
    private static partial Regex BackupDateRegex();

    public record Result(bool Success, List<BackupException> Faild);
}
