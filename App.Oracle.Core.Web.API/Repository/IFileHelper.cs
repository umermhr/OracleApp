
namespace App.Oracle.Core.Web.API.Repository
{
    public interface IFileHelper
    {
        IEnumerable<FileMaster> GetFileList();
        void ReadFileContent();
        IEnumerable<FileContent> GetFileContentByFileId(int fileId);
    }
}