using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.ViewModels;
using Windows.ApplicationModel.Background;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private readonly AppConfig _config;
        private IShellWindow _shellWindow;

        public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService, IIdentityService identityService, IUserDataService userDataService, IOptions<AppConfig> config)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _persistAndRestoreService = persistAndRestoreService;
            _identityService = identityService;
            _userDataService = userDataService;
            _config = config.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

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
            RegisterBackgroundTaskAsync("MyBackgroundTask");
            await Task.CompletedTask;
        }

        private void RegisterBackgroundTaskAsync(string triggerName)
        {
            var current = BackgroundTaskRegistration.AllTasks
                .FirstOrDefault(b => b.Value.Name == triggerName).Value;

            if (current is null)
            {
                var builder = new BackgroundTaskBuilder();
                builder.Name = triggerName;
                builder.SetTrigger(new MaintenanceTrigger(15, false));
                builder.TaskEntryPoint = "MyBackgroundTaskRuntimeComponent.MyBackgroundTask";
                builder.Register();
                System.Diagnostics.Debug.WriteLine("BGTask registered:" + triggerName);
            }
        }
    }
}
