using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.ViewModels;

namespace MyDotNetCoreWpfApp.MVVMLight
{
    public partial class App : Application
    {
        private IApplicationHostService _host;

        public ViewModelLocator Locator
            => Resources["Locator"] as ViewModelLocator;

        public App()
        {
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            AddConfiguration(e.Args);
            _host = SimpleIoc.Default.GetInstance<IApplicationHostService>();
            await _host.StartAsync();
        }

        private void AddConfiguration(string[] args)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();

            Locator.AddConfiguration(configuration);
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
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