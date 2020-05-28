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
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using Windows.UI.Text.Core;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDefaultActivationHandler _defaultActivationHandler;
        private readonly IToastNotificationsService _toastNotificationsService;
        private readonly ISchemeActivationHandler _schemeActivationHandler;
        private readonly INavigationService _navigationService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private readonly IBackgroundTaskService _backgroundTaskService;
        private readonly IFirstRunWindowService _firstRunWindowService;
        private readonly IWhatsNewWindowService _whatsNewWindowService;
        private readonly IUserActivityService _userActivityService;
        private readonly AppConfig _appConfig;
        private bool _isInitialized;

        public ApplicationHostService(IServiceProvider serviceProvider, IDefaultActivationHandler defaultActivationHandler, IToastNotificationsService toastNotificationsService, ISchemeActivationHandler schemeActivationHandler, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IBackgroundTaskService backgroundTaskService, IFirstRunWindowService firstRunWindowService, IWhatsNewWindowService whatsNewWindowService, IUserActivityService userActivityService, IOptions<AppConfig> appConfig)
        {
            _serviceProvider = serviceProvider;
            _defaultActivationHandler = defaultActivationHandler;
            _toastNotificationsService = toastNotificationsService;
            _schemeActivationHandler = schemeActivationHandler;
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _identityService = identityService;
            _userDataService = userDataService;            
            _backgroundTaskService = backgroundTaskService;
            _firstRunWindowService = firstRunWindowService;
            _whatsNewWindowService = whatsNewWindowService;
            _userActivityService = userActivityService;
            _appConfig = appConfig.Value;
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
            var activationHandler = GetActivationHandlers()
                                        .FirstOrDefault(h => h.CanHandle());

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync();
            }

            if (_defaultActivationHandler.CanHandle())
            {
                await _defaultActivationHandler.HandleAsync();
            }
        }

        private IEnumerable<IActivationHandler> GetActivationHandlers()
        {
            yield return _toastNotificationsService;
            yield return _schemeActivationHandler;
        }
    }
}
