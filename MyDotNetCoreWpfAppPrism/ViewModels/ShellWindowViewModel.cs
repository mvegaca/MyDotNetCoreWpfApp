using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfAppPrism.Models;
using MyDotNetCoreWpfAppPrism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfAppPrism.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IRegionNavigationService _navigationService;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { SetProperty(ref _selectedOptionsMenuItem, value); }
        }

        // TODO WTS: Change the icons and titles for all HamburgerMenuItems here.
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", Tag = typeof(Main).Name },
            new HamburgerMenuGlyphItem() { Label = "Blank", Glyph = "\uE8A5", Tag = typeof(Blank).Name }
        };

        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Settings", Glyph = "\uE713", Tag = typeof(Settings).Name }
        };

        public DelegateCommand LoadedCommand { get; private set; }

        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand MenuItemInvokedCommand { get; private set; }

        public DelegateCommand OptionsMenuItemInvokedCommand { get; private set; }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadedCommand = new DelegateCommand(OnLoaded);
            GoBackCommand = new DelegateCommand(OnGoBack, CanGoBack);
            MenuItemInvokedCommand = new DelegateCommand(MenuItemInvoked);
            OptionsMenuItemInvokedCommand = new DelegateCommand(OptionsMenuItemInvoked);
        }                

        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[AppConfig.MainRegion].NavigationService;
            _navigationService.Navigated += OnNavigated;
            _navigationService.RequestNavigate(MenuItems.First().Tag.ToString());
        }

        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        private void OnGoBack()
            => _navigationService.Journal.GoBack();

        private void MenuItemInvoked()
            => _navigationService.RequestNavigate(SelectedMenuItem.Tag.ToString());        

        private void OptionsMenuItemInvoked()
            => _navigationService.RequestNavigate(SelectedOptionsMenuItem.Tag.ToString());

        private void OnNavigated(object sender, RegionNavigationEventArgs e)
        {
            var item = MenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => e.Uri.ToString() == i.Tag.ToString());
            if (item != null)
            {
                SelectedMenuItem = item;
            }
            else
            {
                SelectedOptionsMenuItem = OptionMenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => e.Uri.ToString() == i.Tag.ToString());
            }

            GoBackCommand.RaiseCanExecuteChanged();
        }        
    }
}
