using System;
using System.Runtime.Serialization;

namespace MapleScreenshotBackup
{
    [Serializable]
    public class BackupException : Exception
    {
        public string FileName { get; }

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

        protected BackupException(string faildFileName, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            FileName = faildFileName;
        }
    }
}
