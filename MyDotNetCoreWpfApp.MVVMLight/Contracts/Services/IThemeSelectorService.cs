using MyDotNetCoreWpfApp.MVVMLight.Models;

namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
