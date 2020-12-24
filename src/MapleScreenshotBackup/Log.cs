using System;
using System.Windows.Forms;

namespace MapleScreenshotBackup
{
    public class Log
    {
        private readonly ListBox _logBox;

        public Log(ListBox logBox)
        {
            _logBox = logBox;
        }

        public void WriteLog<T>(T item)
        {
            _logBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {item}");

            var visibleItems = _logBox.ClientSize.Height / _logBox.ItemHeight;
            _logBox.TopIndex = Math.Max(_logBox.Items.Count - visibleItems + 1, 0);
        }
    }
}
