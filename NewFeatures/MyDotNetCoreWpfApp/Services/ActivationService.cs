using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Services
{
    public class ActivationService : IActivationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDefaultActivationHandler _defaultHandler;
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
        private bool _isInitialized = false;

        public ActivationService(IServiceProvider serviceProvider, IDefaultActivationHandler defaultHandler, INavigationService navigationService, IPageService pageService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> config, IBackgroundTaskService backgroundTaskService, IFirstRunWindowService firstRunWindowService, IWhatsNewWindowService whatsNewWindowService, IToastNotificationsService toastNotificationsService)
        {
            _serviceProvider = serviceProvider;
            _defaultHandler = defaultHandler;
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

        public async Task ActivateAsync(string[] activationArgs)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync(activationArgs);

            // Tasks after activation
            await StartupAsync();
            _isInitialized = true;
        }

        public async Task StopAsync()
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
                await Task.CompletedTask;
            }            
        }

        private async Task HandleActivationAsync(string[] activationArgs)
        {
            var activationHandler = GetActivationHandlers()
                                        .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }
            
            if (_defaultHandler.CanHandle(activationArgs))
            {
                await _defaultHandler.HandleAsync(activationArgs);
            }
        }

        private IEnumerable<IActivationHandler> GetActivationHandlers()
        {
            yield return _serviceProvider.GetService(typeof(IToastNotificationsService)) as IToastNotificationsService;
        }
    }
}
