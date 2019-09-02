using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private HamburgerMenuItem _selectedMenuItem;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", Tag = "Main" },
            new HamburgerMenuGlyphItem() { Label = "Secondary", Glyph = "\uE8A5", Tag = "Secondary" }
        };

        public DelegateCommand MenuItemInvokedCommand { get; private set; }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;            
            MenuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigationPath)
            => _regionManager.RequestNavigate("MainRegion", navigationPath, NavigationComplete);

        private void OnMenuItemInvoked()
            => _regionManager.RequestNavigate("MainRegion", SelectedMenuItem.Tag.ToString(), NavigationComplete);        

        private void NavigationComplete(NavigationResult obj)
        {
        }
    }
}