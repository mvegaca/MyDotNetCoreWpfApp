using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface ISystemService
    {
        AppVersion GetVersion();

        void OpenInWebBrowser(string url);
    }
}
