using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Uwp.Notifications;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Contracts.Activation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp
{
    // For more inforation about application lifecyle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
        }

        public T GetService<T>() where T : class
            => _host.Services.GetService(typeof(T)) as T;

        public async Task StartAsync()
            => await _host.StartAsync();

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            // Register AUMID, COM server, and activator
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<ToastNotificationActivator>("MyDotNetCoreWpfApp");
            DesktopNotificationManagerCompat.RegisterActivator<ToastNotificationActivator>();

            // TODO: Register arguments you want to use on App initialization
            var activationArgs = new Dictionary<string, string>
            {
                { ToastNotificationActivationHandler.ActivationArguments, string.Empty},
                { SchemeActivationHandler.ActivationArguments, GetSchemeActivationArgs(e.Args) }
            };

            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // For more information about .NET generic host see https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);
                        c.AddInMemoryCollection(activationArgs);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            if (e.Args.Contains(DesktopNotificationManagerCompat.ToastActivatedLaunchArg))
            {
                // ToastNotificationActivator code will run after this completes and will show a window if necessary.
                return;
            }

            await _host.StartAsync();
        }

        private string GetSchemeActivationArgs(string[] args)
            => args.FirstOrDefault(a => a.StartsWith(SchemeActivationData.ProtocolName));

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // TODO WTS: Register your services, viewmodels and pages here

            // App Host
            services.AddHostedService<ApplicationHostService>();
            services.AddSingleton<IIdentityCacheService, IdentityCacheService>();
            services.AddHttpClient("msgraph", client =>
            {
                client.BaseAddress = new System.Uri("https://graph.microsoft.com/v1.0/");
            });

            // Core Services
            services.AddTransient<IHttpDataService, HttpDataService>();
            services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddTransient<ISampleDataService, SampleDataService>();
            services.AddSingleton<IFileService, FileService>();

            // Activation Handlers
            services.AddSingleton<IActivationHandler, SchemeActivationHandler>();
            services.AddSingleton<IActivationHandler, ToastNotificationActivationHandler>();

            // Services
            services.AddSingleton<IWhatsNewWindowService, WhatsNewWindowService>();
            services.AddSingleton<IFirstRunWindowService, FirstRunWindowService>();
            services.AddSingleton<IBackgroundTaskService, BackgroundTaskService>();
            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            services.AddSingleton<ISystemService, SystemService>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IUserActivityService, UserActivityService>();
            services.AddSingleton<IToastNotificationsService, ToastNotificationsService>();

            // Views and ViewModels
            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellViewModel>();

            services.AddTransient<FirstRunWindow>();
            services.AddTransient<FirstRunViewModel>();

            services.AddTransient<WhatsNewWindow>();
            services.AddTransient<WhatsNewViewModel>();

            services.AddTransient<SchemeActivationSampleViewModel>();
            services.AddTransient<SchemeActivationSamplePage>();

            services.AddTransient<DataGridViewModel>();
            services.AddTransient<DataGridPage>();

            services.AddTransient<ContentGridViewModel>();
            services.AddTransient<ContentGridPage>();

            services.AddTransient<ContentGridDetailViewModel>();
            services.AddTransient<ContentGridDetailPage>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            _host = null;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0

            // e.Handled = true;
        }
    }
}
