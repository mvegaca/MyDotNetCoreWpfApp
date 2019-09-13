using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
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
        private IHost _host;

        public App()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            _host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                    .ConfigureServices(ConfigureServices)
                    .Build();

            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureNavigation();

            var activationService = _host.Services.GetService<IActivationService>();
            await activationService.ActivateAsync(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IFilesService, FilesService>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();

            // Views and ViewModels
            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellWindowViewModel>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();

            services.AddTransient<BlankViewModel>();
            services.AddTransient<BlankPage>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
        }

        private void ConfigureNavigation()
        {
            var navigationService = _host.Services.GetService<INavigationService>();

            navigationService.Configure(typeof(MainViewModel).FullName, typeof(MainPage));
            navigationService.Configure(typeof(BlankViewModel).FullName, typeof(BlankPage));
            navigationService.Configure(typeof(SettingsViewModel).FullName, typeof(SettingsPage));
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            var activationService = _host.Services.GetService<IActivationService>();
            await activationService.ExitAsync();

            _host.Dispose();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Handle the exception before the application will be closed
            // Do whatever you need in case of an unhandled exception was thrown
            // Mark exception as handled
            // e.Handled = true;
        }
    }
}