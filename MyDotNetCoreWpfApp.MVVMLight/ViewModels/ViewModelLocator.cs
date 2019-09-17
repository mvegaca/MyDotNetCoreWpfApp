using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Views;
using MyDotNetCoreWpfApp.MVVMLight.Services;
using MyDotNetCoreWpfApp.MVVMLight.Views;

namespace MyDotNetCoreWpfApp.MVVMLight.ViewModels
{
    public class ViewModelLocator
    {
        private IHost _host;

        //public IActivationService ActivationService
        //    => SimpleIoc.Default.GetInstance<IActivationService>();

        public INavigationService NavigationService
            => SimpleIoc.Default.GetInstance<INavigationService>();

        public ShellWindowViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellWindowViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ViewModelLocator()
        {
            //SimpleIoc.Default.Register<IThemeSelectorService, ThemeSelectorService>();
            //SimpleIoc.Default.Register<IFilesService, FilesService>();
            //SimpleIoc.Default.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();
            //SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            //SimpleIoc.Default.Register<ShellWindowViewModel>();
        }

        public async Task InitializeAsync(string[] args)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                .ConfigureServices(ConfigureServices)
                .Build();

            await _host.StartAsync();
        }

        private void ConfigureServices(IServiceCollection services)
        {
        }

        public void Register<VM, V>()
            where VM : class
            where V : class
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
