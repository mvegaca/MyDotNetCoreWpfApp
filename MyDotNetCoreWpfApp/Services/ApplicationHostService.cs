using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Services
{
    internal class ApplicationHostService : IHostedService
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            _shellWindow.ShowWindow();
            _navigationService.Navigate(typeof(MainViewModel).FullName);

            // Tasks after activation
            await StartupAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
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