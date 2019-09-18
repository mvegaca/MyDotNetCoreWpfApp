using MyDotNetCoreWpfAppPrism.Models;

namespace MyDotNetCoreWpfAppPrism.Contracts.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
