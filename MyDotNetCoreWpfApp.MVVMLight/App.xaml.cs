using System.Windows;
using MyDotNetCoreWpfApp.MVVMLight.ViewModels;

namespace MyDotNetCoreWpfApp.MVVMLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModelLocator Locator
            => Resources["Locator"] as ViewModelLocator;

        public App()
        {
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await Locator.ApplicationHostService.StartAsync();
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await Locator.ApplicationHostService.StopAsync();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0 

            // e.Handled = true;
        }
    }
}
