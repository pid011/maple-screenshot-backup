using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapleScreenshotBackup
{

    public class Backup
    {
        public record Result(bool Success, List<BackupException> Faild);

        public List<(string source, string dest)> ScreenshotsPathCache { get; private set; }

        private readonly string[] _extensions = new string[]
        {
            ".png",
            ".jpg"
        };

        private static readonly AsyncLock s_lock = new AsyncLock();
        private readonly string _screenshotDir;
        private readonly string _backupDir;

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

        public async Task FindScreenshotsAsync(ProgressBar progress)
        {
            using var disposer = await s_lock.LockAsync();

            progress.Style = ProgressBarStyle.Marquee;

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

            progress.Style = ProgressBarStyle.Blocks;

            static string GetBackupPathFromFileName(string filename)
            {
                var matches = Regex.Matches(filename, "([0-9]{6})");
                if (matches.Count != 2)
                {
                    return null;
                }
                var dateStr = matches[0].Value;
                var year = "20" + dateStr.Substring(0, 2);
                var month = dateStr.Substring(2, 2);
                var day = dateStr.Substring(4, 2);

                return Path.Combine(year, month, day);
            }
        }

        public async Task<Result> StartBackupAsync(ProgressBar progress, bool canDelete = false)
        {
            using var disposer = await s_lock.LockAsync();

            var defaultProgress = new
            {
                progress.Maximum,
                progress.Step
            };

            progress.Style = ProgressBarStyle.Blocks;
            progress.Value = 0;
            progress.Maximum = ScreenshotsPathCache.Count;
            progress.Step = 1;

            try
            {
                var result = await Task.Run(() => ProcessBackup(progress, canDelete));
                return result;
            }
            finally
            {
                progress.Value = 0;
                progress.Maximum = defaultProgress.Maximum;
                progress.Step = defaultProgress.Step;
            }
        }

        private Result ProcessBackup(ProgressBar progress, bool canDelete)
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
                            faild.Add(new BackupException(source, $"File name [{dest}] is already exist and It is not same file."));
                            continue;
                        }
                    }

                    // Overwriting file
                    sourceFile.CopyTo(dest, overwrite: true);
                    if (canDelete)
                    {
                        // Send to recycle bin
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                            source,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);

                        // Delete permanently
                        // sourceFile.Delete();
                    }
                }
                catch (Exception e)
                {
                    faild.Add(new BackupException(source, $"Faild to backup screenshot [{source}]", e));
                }
                finally
                {
                    progress.BeginInvoke(new Action(() => progress.PerformStep()));
                }
            }

            return new Result(faild.Count == 0, faild);
        }
    }
}
