using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Views;
using MyDotNetCoreWpfApp.MVVMLight.Models;
using MyDotNetCoreWpfApp.MVVMLight.Services;
using MyDotNetCoreWpfApp.MVVMLight.Views;

namespace MyDotNetCoreWpfApp.MVVMLight.ViewModels
{
    public class ViewModelLocator
    {
        private INavigationService NavigationService
            => SimpleIoc.Default.GetInstance<INavigationService>();

        public ShellWindowViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellWindowViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public BlankViewModel BlankViewModel
            => SimpleIoc.Default.GetInstance<BlankViewModel>();

        public SettingsViewModel SettingsViewModel
            => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IThemeSelectorService, ThemeSelectorService>();
            SimpleIoc.Default.Register<IFilesService, FilesService>();
            SimpleIoc.Default.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ShellWindowViewModel>();
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();
            Register<MainViewModel, MainPage>();
            Register<BlankViewModel, BlankPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        private void Register<VM, V>()
            where VM : class
            where V : class
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }

        public void AddConfiguration(IConfiguration configuration)
        {
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            // Register configurations to IoC
            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register(() => appConfig);
        }
    }
}
