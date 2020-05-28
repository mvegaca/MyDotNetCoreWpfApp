using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Activation
{
    public class SchemeActivationHandler : ISchemeActivationHandler
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private bool IsApplicationStarted
            => App.Current.Windows.Count > 0;

        public SchemeActivationHandler(IConfiguration config, IServiceProvider serviceProvider, INavigationService navigationService)
        {
            _config = config;
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
        }

        public async Task HandleAsync()
        {
            var uri = new Uri(_config[App.SchemeActivationUriArgs]);
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

        public bool CanHandle()
            => !string.IsNullOrEmpty(_config[App.SchemeActivationUriArgs]);
    }
}
