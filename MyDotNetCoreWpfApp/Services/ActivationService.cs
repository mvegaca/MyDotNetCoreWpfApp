using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    internal class ActivationService : IActivationService
    {
        private IThemeSelectorService _themeSelectorService;
        private IPersistAndRestoreService _persistAndRestoreService;
        private INavigationService _navigationService;
        private IShellWindow _shellWindow;

        public ActivationService(IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, INavigationService navigationService, IShellWindow shellWindow)
        {
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _navigationService = navigationService;
            _shellWindow = shellWindow;
            _navigationService.Initialize(_shellWindow.GetNavigationFrame());
        }

        public async Task ActivateAsync(StartupEventArgs activationArgs)
        {
            // Consider user activationArgs...
            // Initialize services that you need before app activation
            await InitializeAsync();

            _shellWindow.ShowWindow();
            if (!_navigationService.IsNavigated())
            {
                _navigationService.Navigate(typeof(MainViewModel).FullName);
            }

            // Tasks after activation
            await StartupAsync();
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

        public async Task ExitAsync()
        {
            await Task.CompletedTask;
            _persistAndRestoreService.PersistData();
        }
    }
}