using System.Threading.Tasks;
using System.Windows;

namespace MyDotNetCoreWpfApp.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(StartupEventArgs args);

        Task HandleAsync(StartupEventArgs args);
    }
}
