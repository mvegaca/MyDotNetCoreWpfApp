using GalaSoft.MvvmLight.Ioc;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfMvvmLightApp.Activation;
using MyDotNetCoreWpfMvvmLightApp.Services;
using MyDotNetCoreWpfMvvmLightApp.Views;
namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class ViewModelLocator
    {
        public IActivationService ActivationService
            => SimpleIoc.Default.GetInstance<IActivationService>();

        public INavigationService NavigationService
            => SimpleIoc.Default.GetInstance<INavigationService>();

        public ShellWindowViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellWindowViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public SecondaryViewModel SecondaryViewModel
            => SimpleIoc.Default.GetInstance<SecondaryViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<DefaultActivationHandler>();
            SimpleIoc.Default.Register<IThemeSelectorService, ThemeSelectorService>();
            SimpleIoc.Default.Register<IFilesService, FilesService>();
            SimpleIoc.Default.Register<IIsolatedStorageService, IsolatedStorageService>();
            SimpleIoc.Default.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            SimpleIoc.Default.Register<IActivationService, ActivationService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();

            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ShellWindowViewModel>();
            Register<MainViewModel, MainPage>();
            Register<SecondaryViewModel, SecondaryPage>();
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
