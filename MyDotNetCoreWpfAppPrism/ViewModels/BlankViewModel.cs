using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfAppPrism.ViewModels
{
    public class BlankViewModel : BindableBase, INavigationAware
    {
        public BlankViewModel(IRegionManager regionManager)
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
