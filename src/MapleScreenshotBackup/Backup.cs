using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapleScreenshotBackup
{
    public record BackupResult(bool Success, List<string> Faild, List<string> Skip);

    public class Backup
    {
        public List<string> ScreenshotsPathCache { get; private set; }

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
            var findTasks = new List<Task<string[]>>(_extensions.Length);
            foreach (var extension in _extensions)
            {
                var task = Task.Run(() =>
                    Directory.GetFiles(_screenshotDir, "*" + extension, SearchOption.TopDirectoryOnly));
                findTasks.Add(task);
            }

            await Task.WhenAll(findTasks);

            var capacity = findTasks.Sum(t => t.Result.Length) + 1;
            ScreenshotsPathCache = new List<string>(capacity);
            findTasks.ForEach(t => ScreenshotsPathCache.AddRange(t.Result));
            progress.Style = ProgressBarStyle.Blocks;
        }

        public async Task<BackupResult> StartBackupAsync(ProgressBar progress, bool canDelete = false)
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

            var result = await Task.Run(() => ProcessBackup(progress, canDelete));

            progress.Value = 0;
            progress.Maximum = defaultProgress.Maximum;
            progress.Step = defaultProgress.Step;

            return result;
        }

        private BackupResult ProcessBackup(ProgressBar progress, bool canDelete)
        {
            var faild = new List<string>();
            var skip = new List<string>();

            foreach (var source in ScreenshotsPathCache)
            {
                try
                {
                    var filename = Path.GetFileName(source);
                    var position = GetBackupPathFromFileName(filename);
                    if (position == null)
                    {
                        faild.Add(source);
                        continue;
                    }

                    var screenshotDir = Path.Combine(_backupDir, position);
                    var dirInfo = Directory.CreateDirectory(screenshotDir);
                    var dest = Path.Combine(screenshotDir, filename);

                    var canCopy = true;

                    var sourceFile = new FileInfo(source);
                    if (File.Exists(dest))
                    {
                        var destFile = new FileInfo(dest);

                        // Do not copy if file name is already exist but file size is diffrent
                        // (Not same file)
                        if (sourceFile.Length != destFile.Length)
                        {
                            skip.Add(source);
                            canCopy = false;
                        }
                    }

                    if (canCopy)
                    {
                        // Overwriting file
                        sourceFile.CopyTo(dest, overwrite: true);
                        if (canDelete)
                        {
                            sourceFile.Delete();
                        }
                    }
                }
                catch (Exception)
                {
                    faild.Add(source);
                }
                finally
                {
                    progress.BeginInvoke(new Action(() => progress.PerformStep()));
                }
            }

            return new BackupResult(faild.Count == 0, faild, skip);

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
    }
}
