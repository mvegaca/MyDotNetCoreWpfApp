using System.Threading.Tasks;

using MyDotNetCoreWpfApp.Core.Models;

namespace MyDotNetCoreWpfApp.Core.Contracts.Services
{
    public interface IMicrosoftGraphService
    {
        Task<User> GetUserInfoAsync(string accessToken);

        Task<string> GetUserPhoto(string accessToken);
    }
}
