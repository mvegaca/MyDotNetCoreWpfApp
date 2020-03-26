using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IBackgroundTaskService
    {
        Task RegisterBackbroundTasksAsync(); 
    }
}
