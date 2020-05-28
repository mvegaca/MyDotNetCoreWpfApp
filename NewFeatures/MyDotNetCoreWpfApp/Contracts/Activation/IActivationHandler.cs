using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}
