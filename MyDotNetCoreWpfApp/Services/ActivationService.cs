using Microsoft.Extensions.DependencyInjection;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    internal class ActivationService
    {
        private DefaultActivationHandler _defaultHandler;
        private ThemeSelectorService _themeSelectorService;
        private ICollection<ActivationHandler> _activationHandlers = new List<ActivationHandler>();

        public ActivationService(DefaultActivationHandler defaultActivationHandler, ThemeSelectorService themeSelectorService)
        {
            _defaultHandler = defaultActivationHandler;
            _themeSelectorService = themeSelectorService;
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
            App.Current.RestoreProperties();
            _themeSelectorService.SetTheme();
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;           
        }

        internal void Exit()
        {
            App.Current.SaveProperties();
        }
    }
}
