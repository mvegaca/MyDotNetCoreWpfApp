using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Activation
{
    internal class DefaultActivationHandler : IActivationHandler
    {
        private INavigationService _navigationService;

        public DefaultActivationHandler(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public async Task HandleAsync(object args)
        {
            await Task.CompletedTask;
            _navigationService.Navigate(typeof(MainViewModel).FullName);
        }
    }
}
