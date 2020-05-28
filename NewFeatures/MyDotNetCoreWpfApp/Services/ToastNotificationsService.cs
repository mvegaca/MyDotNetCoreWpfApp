using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Uwp.Notifications;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.ViewModels;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Services
{
    public partial class ToastNotificationsService : IToastNotificationsService
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private bool IsApplicationStarted
            => App.Current.Windows.Count > 0;

        public ToastNotificationsService(IConfiguration config, IServiceProvider serviceProvider, INavigationService navigationService)
        {
            _config = config;
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
        }

        public bool CanHandle()
            => !string.IsNullOrEmpty(_config[App.ToastNotificationArgs]);

        public async Task HandleAsync()
        {
            if (IsApplicationStarted)
            {
                App.Current.MainWindow.Activate();
                if (App.Current.MainWindow.WindowState == WindowState.Minimized)
                {
                    App.Current.MainWindow.WindowState = WindowState.Normal;
                }

                _navigationService.NavigateTo(typeof(SettingsViewModel).FullName);
            }
            else
            {
                var shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
                _navigationService.Initialize(shellWindow.GetNavigationFrame());
                shellWindow.ShowWindow();
                _navigationService.NavigateTo(typeof(SettingsViewModel).FullName);
            }

            await Task.CompletedTask;
        }
    }
}
