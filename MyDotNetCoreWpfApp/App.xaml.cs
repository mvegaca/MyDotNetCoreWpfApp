using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDotNetCoreWpfApp.Configuration;
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
        private const string _settingsFileName = "settings.json";

        private IActivationService _activationService;
        private INavigationService _navigationService;
        public App()
        {                        
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();
            _activationService = serviceProvider.GetService<IActivationService>();
            _navigationService = serviceProvider.GetService<INavigationService>();
            ConfigureNavigation();

            await _activationService.ActivateAsync(e);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var config = new ConfigurationBuilder()
                            .SetBasePath(Environment.CurrentDirectory)
                            .AddJsonFile(_settingsFileName)
                            .Build();

            // Options
            services.Configure<AppConfig>(config.GetSection(nameof(AppConfig)));

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

            services.AddTransient<SecondaryViewModel>();
            services.AddTransient<SecondaryPage>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();

            return services;
        }

        private void ConfigureNavigation()
        {
            _navigationService.Configure(typeof(MainViewModel).FullName, typeof(MainPage));
            _navigationService.Configure(typeof(SecondaryViewModel).FullName, typeof(SecondaryPage));
            _navigationService.Configure(typeof(SettingsViewModel).FullName, typeof(SettingsPage));
        }

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