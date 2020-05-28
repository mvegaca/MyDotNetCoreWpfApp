using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using Windows.UI.Text.Core;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationHostService : IActivationService
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
        private readonly AppConfig _config;
        private bool _isInitialized;

        public ApplicationHostService(IServiceProvider serviceProvider, IDefaultActivationHandler defaultActivationHandler, IToastNotificationsService toastNotificationsService, ISchemeActivationHandler schemeActivationHandler, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> config, IBackgroundTaskService backgroundTaskService, IFirstRunWindowService firstRunWindowService, IWhatsNewWindowService whatsNewWindowService, IUserActivityService userActivityService)
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
            _config = config.Value;
            _backgroundTaskService = backgroundTaskService;
            _firstRunWindowService = firstRunWindowService;
            _whatsNewWindowService = whatsNewWindowService;
            _userActivityService = userActivityService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync(activationArgs);

            // Tasks after activation
            await StartupAsync();
            _isInitialized = true;
        }

        private async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                _persistAndRestoreService.RestoreData();
                _themeSelectorService.SetTheme();
                _userDataService.Initialize();
                _identityService.InitializeWithAadAndPersonalMsAccounts(_config.IdentityClientId, "http://localhost");
                _identityService.InitializeWebApi(_config.ResourceId, _config.WebApiScope);
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

        private async Task HandleActivationAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers()
                                        .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (_defaultActivationHandler.CanHandle(activationArgs))
            {
                await _defaultActivationHandler.HandleAsync(activationArgs);
            }
        }

        private IEnumerable<IActivationHandler> GetActivationHandlers()
        {
            yield return _toastNotificationsService;
            yield return _schemeActivationHandler;
        }
    }
}
