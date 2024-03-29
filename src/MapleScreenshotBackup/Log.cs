using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapleScreenshotBackup
{
    public class Log
    {
        private readonly ListBox _logBox;
        private readonly Queue<string> _logTexts = new Queue<string>();

        public Log(ListBox logBox)
        {
            _logBox = logBox;
        }

        public void WriteLine()
        {
            AddLog(string.Empty, false);
        }

        public void WriteLine<T>(T item, bool hide = false)
        {
            var text = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {item}";
            AddLog(text, hide);
        }

        private void AddLog(string text, bool hide)
        {
            _logTexts.Enqueue(text);
            if (!hide)
            {
                _logBox.Items.Add(text);
                var visibleItems = _logBox.ClientSize.Height / _logBox.ItemHeight;
                _logBox.TopIndex = Math.Max(_logBox.Items.Count - visibleItems + 1, 0);
            }
        }

        public async Task ExportLogAsync(string path)
        {
            var sb = new StringBuilder();
            foreach (var item in _logTexts)
            {
                sb.AppendLine(item);
            }
            await File.WriteAllTextAsync(path, sb.ToString());
        }
    }
}
