
namespace App.Oracle.Core.Worker.Service.Repository
{
    public interface IBackgroundTasks
    {
        Task<int> CheckFiles();
    }
}