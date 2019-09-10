using System;
using System.Threading.Tasks;
using System.Windows;

namespace MyDotNetCoreWpfApp.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(StartupEventArgs activationArgs);

        Task ExitAsync();
    }
}
