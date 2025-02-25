using System.IO;
using System.Text;
using ListBox = System.Windows.Controls.ListBox;

namespace MapleScreenshotBackup;

public class Log(ListBox logBox)
{
    private readonly ListBox _logBox = logBox;
    private readonly Queue<string> _logTexts = new();

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
            _logBox.ScrollIntoView(_logBox.Items[^1]);
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
