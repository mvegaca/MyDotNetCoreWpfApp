using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using MyDotNetCoreWpfApp.Arguments;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.ViewModels;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Services
{
    public partial class ToastNotificationsService : IToastNotificationsService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private bool IsApplicationStarted
            => App.Current.Windows.Count > 0;

        public ToastNotificationsService(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
        }

        protected override bool CanHandleInternal(ToastNotificationArguments args)
        {

            // args.Contains("ToastContentActivationParams");
            return base.CanHandleInternal(args);
        }

        protected override async Task HandleInternalAsync(ToastNotificationArguments args)
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

        public override void ShowToastNotification(ToastNotification toastNotification)
        {
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
        }
    }
}
