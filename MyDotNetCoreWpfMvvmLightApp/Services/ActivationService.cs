using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDotNetCoreWpfMvvmLightApp.Activation;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public class ActivationService
    {
        private DefaultActivationHandler _defaultHandler;
        private ThemeSelectorService _themeSelectorService;
        PersistAndRestoreService _persistAndRestoreService;
        private ICollection<IActivationHandler> _activationHandlers = new List<IActivationHandler>();

        public ActivationService(DefaultActivationHandler defaultActivationHandler, ThemeSelectorService themeSelectorService, PersistAndRestoreService persistAndRestoreService)
        {
            _defaultHandler = defaultActivationHandler;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _activationHandlers.Add(_persistAndRestoreService);
        }

        public async Task ActivateAsync(object activationArgs)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

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
            FilesService.Initialize();
            App.Current.RestoreProperties();
            _themeSelectorService.SetTheme();
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;
        }

        public async Task ExitAsync()
        {
            await _persistAndRestoreService.PersistDataAsync();
            App.Current.SaveProperties();
        }
    }
}
