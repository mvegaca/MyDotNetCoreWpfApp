using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.Services
{
    public interface IApplicationHostService
    {
        Task StartAsync();

        Task StopAsync();
    }
}
