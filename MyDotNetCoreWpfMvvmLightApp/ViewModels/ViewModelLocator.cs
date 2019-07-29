using GalaSoft.MvvmLight.Ioc;
using MyDotNetCoreWpfMvvmLightApp.Activation;
using MyDotNetCoreWpfMvvmLightApp.Services;
using MyDotNetCoreWpfMvvmLightApp.Views;
namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class ViewModelLocator
    {
        public ActivationService ActivationService
            => SimpleIoc.Default.GetInstance<ActivationService>();

        public NavigationService NavigationService
            => SimpleIoc.Default.GetInstance<NavigationService>();

        public ShellViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public SecondaryViewModel SecondaryViewModel
            => SimpleIoc.Default.GetInstance<SecondaryViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<DefaultActivationHandler>();
            SimpleIoc.Default.Register<ThemeSelectorService>();
            SimpleIoc.Default.Register<PersistAndRestoreService>();
            SimpleIoc.Default.Register<ActivationService>();
            SimpleIoc.Default.Register<NavigationService>();

            Register<ShellWindow, ShellViewModel>();
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
