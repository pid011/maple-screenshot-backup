using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MapleScreenshotBackup
{
    public class Backup
    {
        public List<string> ScreenshotsPathCache { get; private set; }

        private readonly string[] _extensions = new string[]
        {
            "*.png",
            "*.jpg"
        };
        private readonly BackupDirectories _directories;

        public Backup(BackupDirectories directories)
        {
            if (directories is null || string.IsNullOrWhiteSpace(directories.BackupDirectory) || string.IsNullOrWhiteSpace(directories.MapleDirectory))
            {
                throw new ArgumentException("Wrong directories.");
            }

            _directories = directories;
        }

        public async Task<bool> FindScreenshotsAsync()
        {
            var findTasks = new List<Task<string[]>>(_extensions.Length);
            foreach (var extension in _extensions)
            {
                var task = Task.Run(() =>
                    Directory.GetFiles(_directories.MapleDirectory, extension, SearchOption.TopDirectoryOnly));
                findTasks.Add(task);
            }

            await Task.WhenAll(findTasks);

            var capacity = findTasks.Sum(t => t.Result.Length) + 1;
            ScreenshotsPathCache = new List<string>(capacity);
            findTasks.ForEach(t => ScreenshotsPathCache.AddRange(t.Result));

            return true;
        }
    }
}
