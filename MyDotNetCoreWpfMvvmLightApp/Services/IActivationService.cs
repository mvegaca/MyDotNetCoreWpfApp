using System.Threading.Tasks;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public interface IActivationService
    {

        Task ActivateAsync(object activationArgs);

        Task ExitAsync();
    }
}
