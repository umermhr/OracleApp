namespace App.Oracle.Core.Shared.Models
{
    public class LogFile
    {
        public string? FileName { get; set; }
        public string? LogFilePath { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public List<LogFileContent>? FileContents { get; set; }
    }

    public class LogFileContent
    {
        public int LineNo { get; set; } = 0;
        public string? LineContent { get; set; }
    }
}
