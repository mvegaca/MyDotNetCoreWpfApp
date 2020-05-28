using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Models;
using Windows.UI.Shell;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IUserActivityService
    {
        Task CreateUserActivityAsync(UserActivityData activityData);

        Task CreateUserActivityAsync(UserActivityData activityData, IAdaptiveCard adaptiveCard);

        Task AddSampleUserActivityAsync();
    }
}
