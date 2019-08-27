using System.Threading.Tasks;
using MyDotNetCoreWpfMvvmLightApp.Services;
using MyDotNetCoreWpfMvvmLightApp.ViewModels;

namespace MyDotNetCoreWpfMvvmLightApp.Activation
{
    public class DefaultActivationHandler : IActivationHandler
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
