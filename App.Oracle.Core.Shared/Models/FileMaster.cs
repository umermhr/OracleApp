namespace App.Oracle.Core.Shared.Models
{
    public class FileMaster
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public DateTime? FileCreationTime { get; set; }
        public DateTime? RecordCreationTime { get; set; }
        public string? RecordCreatedBy { get; set; }

    }
}
