using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IActivationService : IHostedService
    {
        Task ActivateAsync(string[] activationArgs);
    }
}
