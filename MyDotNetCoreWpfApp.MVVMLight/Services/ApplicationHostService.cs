using System.Threading.Tasks;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Views;
using MyDotNetCoreWpfApp.MVVMLight.ViewModels;

namespace MyDotNetCoreWpfApp.MVVMLight.Services
{
    public class ApplicationHostService : IApplicationHostService
    {
        private readonly INavigationService _navigationService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IShellWindow _shellWindow;

        public ApplicationHostService(INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IShellWindow shellWindow)
        {
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _shellWindow = shellWindow;
            _navigationService.Initialize(_shellWindow.GetNavigationFrame());
        }

        public async Task StartAsync()
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            _shellWindow.ShowWindow();
            _navigationService.NavigateTo(typeof(MainViewModel).FullName);

            // Tasks after activation
            await StartupAsync();
        }

        public async Task StopAsync()
        {
            await Task.CompletedTask;
            _persistAndRestoreService.PersistData();
        }

        private async Task InitializeAsync()
        {
            await Task.CompletedTask;
            _persistAndRestoreService.RestoreData();
            _themeSelectorService.SetTheme();
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;
        }
    }
}