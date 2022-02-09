
namespace App.Oracle.Core.Web.API.Repository
{
    public interface IFileHelper
    {
        IEnumerable<FileMaster> GetFileList();
        LogFile ReadLogFile();
        string GetLogFilePath();
        void ReadFileContent();
        IEnumerable<FileContent> GetFileContentByFileId(int fileId);
    }
}