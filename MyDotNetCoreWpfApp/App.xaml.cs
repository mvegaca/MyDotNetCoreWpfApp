using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MyDotNetCoreWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static App CurrentApp = (App)Current;

        private Lazy<ActivationService> _activationService;

        internal ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        internal readonly NavigationService NavigationService = new NavigationService();

        public App()
        {
            _activationService = new Lazy<ActivationService>(CreateActivationService);
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private ActivationService CreateActivationService()
            =>new ActivationService(typeof(MainPage), new Lazy<Window>(CreateShell));

        private Window CreateShell() => new ShelWindow();

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            // TODO: Restore application-scope property from isolated storage
            await ActivationService.ActivateAsync(e);
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // TODO: Persist application-scope property to isolated storage
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}
