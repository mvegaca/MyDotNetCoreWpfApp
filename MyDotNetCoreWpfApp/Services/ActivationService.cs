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
        private ICollection<ActivationHandler> _activationHandlers = new List<ActivationHandler>();

        public ActivationService(DefaultActivationHandler defaultActivationHandler)
        {
            _defaultHandler = defaultActivationHandler;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (_defaultHandler.CanHandle(activationArgs))
            {
                await _defaultHandler.HandleAsync(activationArgs);
            }
        }
    }
}
