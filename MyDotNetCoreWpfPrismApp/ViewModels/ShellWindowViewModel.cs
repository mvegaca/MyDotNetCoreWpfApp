using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public DelegateCommand GoBackCommand { get; private set; }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;            
            MenuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            GoBackCommand = new DelegateCommand(OnGoBack, CanGoBack);
        }

        internal void Initialize()
        {
            //_regionManager.Regions["MainRegion"].NavigationService.Navigated += OnNavigated;
        }

        private void OnNavigated(object? sender, RegionNavigationEventArgs e)
            => GoBackCommand.RaiseCanExecuteChanged();

        private void Navigate(string navigationPath)
            => _regionManager.RequestNavigate("MainRegion", navigationPath, NavigationComplete);

        private void OnMenuItemInvoked()
            => _regionManager.RequestNavigate("MainRegion", SelectedMenuItem.Tag.ToString(), NavigationComplete);

        private bool CanGoBack()
        {
            if (_regionManager.Regions.Any())
            {
                var mainRegion = _regionManager.Regions["MainRegion"];
                if (mainRegion != null)
                {
                    return mainRegion.NavigationService.Journal.CanGoBack;
                }
            }            

            return false;
        }

        private void OnGoBack()
            => _regionManager.Regions["MainRegion"].NavigationService.Journal.GoBack();

        private void NavigationComplete(NavigationResult obj)
        {
        }
    }
}