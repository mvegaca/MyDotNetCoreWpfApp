using System.Threading.Tasks;

namespace MyDotNetCoreWpfMvvmLightApp.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }
}
