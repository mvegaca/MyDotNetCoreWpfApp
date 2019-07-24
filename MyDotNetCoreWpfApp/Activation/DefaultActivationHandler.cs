using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Activation
{
    internal class DefaultActivationHandler : ActivationHandler
    {
        private NavigationService _navigationService;
        private ShelWindow _shelWindow;

        public DefaultActivationHandler(NavigationService navigationService, ShelWindow shelWindow)
        {
            _navigationService = navigationService;
            _shelWindow = shelWindow;
        }

        public override bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public override async Task HandleAsync(object args)
        {
            await Task.CompletedTask;
            _shelWindow.Show();
            _navigationService.Navigate<MainPage>();
        }
    }
}
