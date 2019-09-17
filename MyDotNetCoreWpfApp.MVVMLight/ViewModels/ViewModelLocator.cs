using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
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
        private IShellWindow _shell
            => SimpleIoc.Default.GetInstance<IShellWindow>();

        private IPersistAndRestoreService _persistAndRestoreService
            => SimpleIoc.Default.GetInstance<IPersistAndRestoreService>();

        private IThemeSelectorService _themeSelectorService
            => SimpleIoc.Default.GetInstance<IThemeSelectorService>();

        public INavigationService NavigationService
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
            Register<MainViewModel, MainPage>();
            Register<BlankViewModel, BlankPage>();
            Register<SettingsViewModel, SettingsPage>();            
        }

        public async Task StartAsync()
        {
            // Tasks before activation
            await InitializeAsync();

            _shell.ShowWindow();
            NavigationService.NavigateTo(typeof(MainViewModel).FullName);

            // Tasks after activation
            await StartupAsync();
        }

        private async Task InitializeAsync()
        {
            var frame = _shell.GetNavigationFrame();
            NavigationService.Initialize(frame);
            _persistAndRestoreService.RestoreData();
            _themeSelectorService.SetTheme();
            await Task.CompletedTask;
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;
        }

        private void Register<VM, V>()
            where VM : class
            where V : class
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
