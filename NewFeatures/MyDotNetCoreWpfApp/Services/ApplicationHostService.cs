using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.ViewModels;
using Windows.UI.Text.Core;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IActivationHandler> _activationHandlers;
        private readonly INavigationService _navigationService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private readonly IBackgroundTaskService _backgroundTaskService;
        private readonly IFirstRunWindowService _firstRunWindowService;
        private readonly IWhatsNewWindowService _whatsNewWindowService;
        private readonly IUserActivityService _userActivityService;
        private readonly IToastNotificationsService _toastNotificationsService;
        private readonly AppConfig _appConfig;
        private bool _isInitialized;

        public ApplicationHostService(IServiceProvider serviceProvider, IEnumerable<IActivationHandler> activationHandlers, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> appConfig, IBackgroundTaskService backgroundTaskService, IFirstRunWindowService firstRunWindowService, IWhatsNewWindowService whatsNewWindowService, IUserActivityService userActivityService, IToastNotificationsService toastNotificationsService)
        {
            _serviceProvider = serviceProvider;
            _activationHandlers = activationHandlers;
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _identityService = identityService;
            _userDataService = userDataService;
            _appConfig = appConfig.Value;
            _backgroundTaskService = backgroundTaskService;
            _firstRunWindowService = firstRunWindowService;
            _whatsNewWindowService = whatsNewWindowService;
            _userActivityService = userActivityService;
            _toastNotificationsService = toastNotificationsService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync();

            // Tasks after activation
            await StartupAsync();
            _isInitialized = true;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }


        private async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                _persistAndRestoreService.RestoreData();
                _themeSelectorService.SetTheme();
                _userDataService.Initialize();
                _identityService.InitializeWithAadAndPersonalMsAccounts(_appConfig.IdentityClientId, "http://localhost");
                _identityService.InitializeWebApi(_appConfig.ResourceId, _appConfig.WebApiScope);
                await _identityService.AcquireTokenSilentAsync();
            }
        }

        private async Task StartupAsync()
        {
            if (!_isInitialized)
            {
                //await _backgroundTaskService.RegisterBackbroundTasksAsync();
                _firstRunWindowService.ShowIfAppropriate();
                _whatsNewWindowService.ShowIfAppropriate();
                _toastNotificationsService.ShowToastNotificationSample();
                await _userActivityService.AddSampleUserActivityAsync();
                await Task.CompletedTask;
            }
        }

        private async Task HandleActivationAsync()
        {
            var activationHandler = _activationHandlers
                                        .FirstOrDefault(h => h.CanHandle());

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync();
            }

            if (App.Current.Windows.Count == 0)
            {
                var shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
                _navigationService.Initialize(shellWindow.GetNavigationFrame());
                shellWindow.ShowWindow();
                _navigationService.NavigateTo(typeof(MainViewModel).FullName);
            }
        }
    }
}
