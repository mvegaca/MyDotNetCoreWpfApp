using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IActivationService _activationService;
        private INavigationService _navigationService;

        public App()
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();
            _activationService = serviceProvider.GetService<IActivationService>();
            _navigationService = serviceProvider.GetService<INavigationService>();
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            ConfigureNavigation();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IIsolatedStorageService, IsolatedStorageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IFilesService, FilesService>();

            // Handlers
            services.AddTransient<DefaultActivationHandler>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();

            // Views
            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellWindowViewModel>();
            Register<MainViewModel, MainPage>(services, true);
            Register<SecondaryViewModel, SecondaryPage>(services);

            return services;
        }

        private void ConfigureNavigation()
        {
            _navigationService.Configure(typeof(MainViewModel).FullName, typeof(MainPage));
            _navigationService.Configure(typeof(SecondaryViewModel).FullName, typeof(SecondaryPage));
        }

        private static void Register<VM, V>(ServiceCollection services, bool isSingletonPage = false)
            where VM : class
            where V : class
        {
            if (isSingletonPage)
            {
                services.AddSingleton<VM>();
                services.AddSingleton<V>();
            }
            else
            {
                services.AddTransient<VM>();
                services.AddTransient<V>();
            }            
        }

        private async void OnStartup(object sender, StartupEventArgs e)
            => await _activationService.ActivateAsync(e);

        private async void OnExit(object sender, ExitEventArgs e)
            => await _activationService.ExitAsync();

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Handle the exception before the application will be closed
            // Do whatever you need in case of an unhandled exception was thrown
            // Mark exception as handled
            // e.Handled = true;
        }
    }
}