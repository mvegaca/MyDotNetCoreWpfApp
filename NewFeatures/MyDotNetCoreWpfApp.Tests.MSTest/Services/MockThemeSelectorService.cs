using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Tests.MSTest.Services
{
    public class MockThemeSelectorService : IThemeSelectorService
    {
        private AppTheme _theme;

        public AppTheme GetCurrentTheme()
        {
            return _theme;
        }

        public bool SetTheme(AppTheme? theme = null)
        {
            if (theme != null)
            {
                _theme = theme.Value;
            }

            return true;
        }
    }
}
