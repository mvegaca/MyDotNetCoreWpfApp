using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfPrismApp.Helpers;
using MyDotNetCoreWpfPrismApp.Services;
using MyDotNetCoreWpfPrismApp.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;

namespace MyDotNetCoreWpfPrismApp
{
    public partial class App : PrismApplication
    {
        public App()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        protected override Window CreateShell()
            => Container.Resolve<ShellWindow>();

        public override void Initialize()
        {
            base.Initialize();
            var storageService = Container.Resolve<IIsolatedStorageService>();
            var properties = storageService.ReadLines(FileNames.AppProperties);
            this.SetProperties(properties);

            var themeSelectorService = Container.Resolve<IThemeSelectorService>();
            themeSelectorService.SetTheme();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Core Services
            containerRegistry.Register<IFilesService, FilesService>();
            containerRegistry.Register<IIsolatedStorageService, IsolatedStorageService>();

            // App Services
            containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();            
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();

            // Views
            containerRegistry.RegisterForNavigation<Main>();
            containerRegistry.RegisterForNavigation<Secondary>();
        }

        //protected override void ConfigureViewModelLocator()
        //{
        //    base.ConfigureViewModelLocator();
        //    ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        //    {
        //        var viewName = viewType.Name.Substring(0, viewType.Name.Length - 4);
        //        var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "MyDotNetCoreWpfPrismApp.ViewModels.{0}ViewModel, MyDotNetCoreWpfPrismApp", viewName);
        //        return Type.GetType(viewModelTypeName);
        //    });
        //}

        private void OnExit(object sender, ExitEventArgs e)
        {
            var properties = this.GetProperties();
            var storageService = Container.Resolve<IIsolatedStorageService>();
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            storageService.SaveLines(FileNames.AppProperties, properties);
            persistAndRestoreService.PersistData();
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
