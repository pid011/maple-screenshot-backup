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
        private readonly ConfigItem _directories;

        public Backup(ConfigItem directories)
        {
            if (directories is null || string.IsNullOrWhiteSpace(directories.BackupFolder) || string.IsNullOrWhiteSpace(directories.ScreenshotFolder))
            {
                throw new ArgumentException("Wrong directories.");
            }

            _directories = directories;
        }

        public async Task FindScreenshotsAsync(ProgressBar progress)
        {
            progress.Style = ProgressBarStyle.Marquee;

            var findTasks = new List<Task<string[]>>(_extensions.Length);
            foreach (var extension in _extensions)
            {
                var task = Task.Run(() =>
                    Directory.GetFiles(_directories.ScreenshotFolder, "*" + extension, SearchOption.TopDirectoryOnly));
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
            var defaultProgress = new
            {
                progress.Maximum,
                progress.Step
            };

            progress.Style = ProgressBarStyle.Blocks;
            progress.Value = 0;
            progress.Maximum = ScreenshotsPathCache.Count;
            progress.Step = 1;

            var result = await Task.Run(() =>
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

                        var screenshotDir = Path.Combine(_directories.BackupFolder, position);
                        var dirInfo = Directory.CreateDirectory(screenshotDir);
                        var dest = Path.Combine(screenshotDir, filename);

                        var sourceFile = new FileInfo(source);
                        if (File.Exists(dest))
                        {
                            skip.Add(source);
                        }
                        else
                        {
                            _ = sourceFile.CopyTo(dest);
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
            });

            progress.Value = 0;
            progress.Maximum = defaultProgress.Maximum;
            progress.Step = defaultProgress.Step;

            return result;

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
