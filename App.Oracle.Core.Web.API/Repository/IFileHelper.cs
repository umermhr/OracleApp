
namespace App.Oracle.Core.Web.API.Repository
{
    public interface IFileHelper
    {
        IEnumerable<FileMaster> GetFileList();
        string[]? ReadFileContent(string filePath);
    }
}