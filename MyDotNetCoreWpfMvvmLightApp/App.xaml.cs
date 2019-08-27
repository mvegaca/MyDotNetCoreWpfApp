using System.Windows;
using System.Windows.Threading;
using MyDotNetCoreWpfMvvmLightApp.ViewModels;

namespace MyDotNetCoreWpfMvvmLightApp
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
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private async void OnStartup(object sender, StartupEventArgs e)
            => await Locator.ActivationService.ActivateAsync(e);

        private async void OnExit(object sender, ExitEventArgs e)
            => await Locator.ActivationService.ExitAsync();

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Handle the exception before the application will be closed
            // Do whatever you need in case of an unhandled exception was thrown
            // Mark exception as handled
            // e.Handled = true;
        }
    }
}
