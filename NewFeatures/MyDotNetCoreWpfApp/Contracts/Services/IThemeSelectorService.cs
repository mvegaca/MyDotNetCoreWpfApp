using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
