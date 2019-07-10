using MyDotNetCoreWpfApp.Activation;
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
        private Lazy<Window> _shell;
        private Type _defaultNavItem;

        public ActivationService(Type defaultNavItem, Lazy<Window> shell)
        {
            _defaultNavItem = defaultNavItem;
            _shell = shell;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers()
                                    .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            var defaultHandler = new DefaultActivationHandler(_defaultNavItem, _shell);
            if (defaultHandler.CanHandle(activationArgs))
            {
                await defaultHandler.HandleAsync(activationArgs);
            }
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }
    }
}
