using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfAppPrism.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        public MainViewModel(IRegionManager regionManager)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
