using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }
}
