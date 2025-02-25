namespace MapleScreenshotBackup;

[Serializable]
public class BackupException : Exception
{
    public BackupException(string faildFileName)
    {
        FileName = faildFileName;
    }

    public BackupException(string faildFileName, Exception inner) : base(null, inner)
    {
        FileName = faildFileName;
    }

    public BackupException(string faildFileName, string message) : base(message)
    {
        FileName = faildFileName;
    }

    public BackupException(string faildFileName, string message, Exception inner) : base(message, inner)
    {
        FileName = faildFileName;
    }

    public string FileName { get; }
}
