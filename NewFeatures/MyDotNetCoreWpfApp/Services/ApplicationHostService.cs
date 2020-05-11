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
        private readonly INavigationService _navigationService;
        private readonly IPageService _pageService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private readonly IBackgroundTaskService _backgroundTaskService;
        private readonly IFirstRunWindowService _firstRunWindowService;
        private readonly IWhatsNewWindowService _whatsNewWindowService;
        private readonly IToastNotificationsService _toastNotificationsService;
        private readonly AppConfig _config;
        private IShellWindow _shellWindow;

        public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IPageService pageService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> config, IBackgroundTaskService backgroundTaskService, IFirstRunWindowService firstRunWindowService, IWhatsNewWindowService whatsNewWindowService, IToastNotificationsService toastNotificationsService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            _pageService = pageService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _identityService = identityService;
            _userDataService = userDataService;
            _config = config.Value;
            _backgroundTaskService = backgroundTaskService;
            _firstRunWindowService = firstRunWindowService;
            _whatsNewWindowService = whatsNewWindowService;
            _toastNotificationsService = toastNotificationsService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            _identityService.InitializeWithAadAndPersonalMsAccounts(_config.IdentityClientId, "http://localhost");
            _identityService.InitializeWebApi(_config.ResourceId, _config.WebApiScope);
            await _identityService.AcquireTokenSilentAsync();

            _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
            _navigationService.Initialize(_shellWindow.GetNavigationFrame());
            _shellWindow.ShowWindow();

            _navigationService.NavigateTo(_pageService.GetDefaultNavigation());

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
            //await _backgroundTaskService.RegisterBackbroundTasksAsync();
            _firstRunWindowService.ShowIfAppropriate();
            _whatsNewWindowService.ShowIfAppropriate();
            _toastNotificationsService.ShowToastNotificationSample();
            await Task.CompletedTask;
        }
    }
}
