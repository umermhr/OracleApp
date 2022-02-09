namespace App.Oracle.Core.Shared.Models
{
    public class FileDownload
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
