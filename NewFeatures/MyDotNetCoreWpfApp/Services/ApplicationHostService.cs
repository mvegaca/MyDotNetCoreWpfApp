using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.Notifications;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly INavigationService _navigationService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private readonly IBackgroundTaskService _backgroundTaskService;
        private readonly INotificationsService _notificationsService;
        private readonly AppConfig _config;
        private IShellWindow _shellWindow;

        public ApplicationHostService(IServiceProvider serviceProvider, IConfigurationProvider configurationProvider, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> config, IBackgroundTaskService backgroundTaskService, INotificationsService notificationsService)
        {
            _serviceProvider = serviceProvider;
            _configurationProvider = configurationProvider;
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _identityService = identityService;
            _userDataService = userDataService;
            _config = config.Value;
            _backgroundTaskService = backgroundTaskService;
            _notificationsService = notificationsService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();


            // Register AUMID, COM server, and activator
            _notificationsService.RegisterAumidAndComServer<MyNotificationActivator>("WindowsNotifications.DesktopToasts");
            _notificationsService.RegisterActivator<MyNotificationActivator>();
            string toastArgs;
            if (_configurationProvider.TryGet(NotificationsService.TOAST_ACTIVATED_LAUNCH_ARG, out toastArgs))
            {
                // Our NotificationActivator code will run after this completes,
                // and will show a window if necessary.
            }
            else
            {
                // Show the window
                // In App.xaml, be sure to remove the StartupUri so that a window doesn't
                // get created by default, since we're creating windows ourselves (and sometimes we
                // don't want to create a window if handling a background activation).
                // new MainWindow().Show();
            }

            _identityService.InitializeWithAadAndPersonalMsAccounts(_config.IdentityClientId, "http://localhost");
            await _identityService.AcquireTokenSilentAsync();

            _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
            _navigationService.Initialize(_shellWindow.GetNavigationFrame());
            _shellWindow.ShowWindow();
            _navigationService.NavigateTo(typeof(MainViewModel).FullName);

            // Tasks after activation
            await StartupAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _persistAndRestoreService.PersistData();
            await Task.CompletedTask;
        }

        private async Task InitializeAsync()
        {
            _persistAndRestoreService.RestoreData();
            _themeSelectorService.SetTheme();
            _userDataService.Initialize();
            await Task.CompletedTask;
        }

        private async Task StartupAsync()
        {
            await _backgroundTaskService.RegisterBackbroundTasksAsync();
            await Task.CompletedTask;
        }
    }
}
