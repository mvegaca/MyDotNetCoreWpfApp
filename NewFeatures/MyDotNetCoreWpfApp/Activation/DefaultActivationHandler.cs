using System;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Activation
{
    public class DefaultActivationHandler : IDefaultActivationHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        public DefaultActivationHandler(IServiceProvider serviceProvider, INavigationService navigationService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
        }

        protected override bool CanHandleInternal(string[] args)
            => App.Current.Windows.Count == 0;

        protected override async Task HandleInternalAsync(string[] args)
        {
            var shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
            _navigationService.Initialize(shellWindow.GetNavigationFrame());
            shellWindow.ShowWindow();
            _navigationService.NavigateTo(typeof(MainViewModel).FullName);
            await Task.CompletedTask;
        }
    }
}
