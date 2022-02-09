namespace App.Oracle.Core.Shared.Models
{
    public class FileContent
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int LineNo { get; set; }
        public string? LineContent { get; set; }
        public DateTime? RecordCreationTime { get; set; }
        public string? RecordCreatedBy { get; set; }
    }
}
