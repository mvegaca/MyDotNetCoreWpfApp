using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.Activation
{
    internal class DefaultActivationHandler : ActivationHandler
    {
        private Type _defaultNavItem;
        private Lazy<Window> _shell;

        public DefaultActivationHandler(Type defaultNavItem, Lazy<Window> shell)
        {
            _defaultNavItem = defaultNavItem;
            _shell = shell;
        }

        public override bool CanHandle(object args)
        {
            return App.CurrentApp.MainWindow == null;
        }

        public override async Task HandleAsync(object args)
        {
            await Task.CompletedTask;
            var shell = _shell.Value;
            shell.Show();
            var frame = shell.Content as Frame;
            App.CurrentApp.NavigationService.Initialize(frame);
            App.CurrentApp.NavigationService.Navigate(_defaultNavItem);
        }
    }
}
