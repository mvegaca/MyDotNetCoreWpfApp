using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    internal class ActivationService : IActivationService
    {
        private DefaultActivationHandler _defaultHandler;
        private IThemeSelectorService _themeSelectorService;
        private IPersistAndRestoreService _persistAndRestoreService;
        private INavigationService _navigationService;
        private ICollection<IActivationHandler> _activationHandlers = new List<IActivationHandler>();
        private IShellWindow _shellWindow;

        public ActivationService(DefaultActivationHandler defaultActivationHandler, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, INavigationService navigationService, IShellWindow shellWindow)
        {
            _defaultHandler = defaultActivationHandler;
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

            var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

            _shellWindow.ShowWindow();

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (_defaultHandler.CanHandle(activationArgs))
            {
                await _defaultHandler.HandleAsync(activationArgs);
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