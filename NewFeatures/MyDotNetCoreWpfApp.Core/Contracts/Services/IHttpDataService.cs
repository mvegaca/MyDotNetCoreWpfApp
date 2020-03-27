using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Core.Contracts.Services
{
    public interface IHttpDataService
    {
        void Initialize(string httpClientName, string defaultBaseUrl);

        Task<T> GetAsync<T>(string uri, string accessToken = null, bool forceRefresh = false);

        Task<bool> PostAsync<T>(string uri, T item);

        Task<bool> PostAsJsonAsync<T>(string uri, T item);

        Task<bool> PutAsync<T>(string uri, T item);

        Task<bool> PutAsJsonAsync<T>(string uri, T item);

        Task<bool> DeleteAsync(string uri);
    }
}
