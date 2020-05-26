using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(string[] args);

        Task HandleAsync(string[] args);
    }
}
