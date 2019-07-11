using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Activation
{
    internal class DefaultActivationHandler : ActivationHandler
    {
        private NavigationService _navigationService;

        public DefaultActivationHandler(NavigationService navigationService)
        {
            _navigationService = navigationService;            
        }

        public override bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public override async Task HandleAsync(object args)
        {
            await Task.CompletedTask;
            _navigationService.Show();
            _navigationService.Navigate<MainPage>();
        }
    }
}
