using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Activation
{
    public class SchemeActivationHandler : ISchemeActivationHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private bool IsApplicationStarted
            => App.Current.Windows.Count > 0;

        public SchemeActivationHandler(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
        }

        protected override async Task HandleInternalAsync(string[] args)
        {
            var uri = new Uri(args.First());
            // Create data from activation Uri in ProtocolActivatedEventArgs
            var data = new SchemeActivationData(uri);
            if (data.IsValid)
            {
                if (IsApplicationStarted)
                {
                    App.Current.MainWindow.Activate();
                    if (App.Current.MainWindow.WindowState == WindowState.Minimized)
                    {
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                    }
                }
                else
                {
                    var shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
                    _navigationService.Initialize(shellWindow.GetNavigationFrame());
                    shellWindow.ShowWindow();
                }

                _navigationService.NavigateTo(data.ViewModelName, data.Parameters);
            }

            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(string[] args)
            => args.Any(a => Uri.TryCreate(a, UriKind.RelativeOrAbsolute, out var activationUri));
        
    }
}
