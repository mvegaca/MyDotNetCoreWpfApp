using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Services
{
    public interface IActivationService
    {

        Task ActivateAsync(object activationArgs);

        Task ExitAsync();
    }
}
