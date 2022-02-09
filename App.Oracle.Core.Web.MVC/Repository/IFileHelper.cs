
namespace App.Oracle.Core.Web.MVC.Repository
{
    public interface IFileHelper
    {
        Task<FileDownload> DownloadLogFileAsync();
        Task<FileContent> GetFileContentAsync(int fileId);
        Task<IEnumerable<FileMaster>> GetListAsync();
        Task<LogFile> GetLogFileAsync(int fileId);
    }
}