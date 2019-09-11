using System.Threading.Tasks;
using System.Windows;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(StartupEventArgs activationArgs);

        Task ExitAsync();
    }
}
