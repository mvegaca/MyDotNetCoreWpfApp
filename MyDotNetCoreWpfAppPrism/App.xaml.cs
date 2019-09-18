using System.Windows;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfAppPrism.Contracts.Services;
using MyDotNetCoreWpfAppPrism.Services;
using MyDotNetCoreWpfAppPrism.Views;
using Prism.Ioc;
using Prism.Unity;

namespace MyDotNetCoreWpfAppPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        protected override Window CreateShell()
            => Container.Resolve<ShellWindow>();

        public override void Initialize()
        {
            base.Initialize();
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.RestoreData();

            var themeSelectorService = Container.Resolve<IThemeSelectorService>();
            themeSelectorService.SetTheme();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Core Services
            containerRegistry.Register<IFilesService, FilesService>();

            // App Services
            containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();

            // Views            
            containerRegistry.RegisterForNavigation<ShellWindow>();
            containerRegistry.RegisterForNavigation<Main>();
            containerRegistry.RegisterForNavigation<Blank>();
            containerRegistry.RegisterForNavigation<Settings>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.PersistData();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Handle the exception before the application will be closed
            // Do whatever you need in case of an unhandled exception was thrown
            // Mark exception as handled
            // e.Handled = true;
        }
    }
}
