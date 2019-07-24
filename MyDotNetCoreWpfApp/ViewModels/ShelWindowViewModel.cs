using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class ShelWindowViewModel : Observable, IDisposable
    {
        private NavigationService _navigationService;
        private RelayCommand _goBackCommand;
        private ICommand _menuItemInvokedCommand;
        private HamburgerMenuItem _selectedMenuItem;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { Set(ref _selectedMenuItem, value); }
        }

        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", TargetPageType = typeof(MainPage) },
            new HamburgerMenuGlyphItem() { Label = "Secondary", Glyph = "\uE8A5", TargetPageType = typeof(SecondaryPage) }
        };

        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(MenuItemInvoked));

        public ShelWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.Navigated += OnNavigated;
        }

        public void Dispose()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            => _navigationService.GoBack();

        private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var pageType = e.Content.GetType();
            SelectedMenuItem = MenuItems
                                .OfType<HamburgerMenuItem>()
                                .First(i => i.TargetPageType == pageType);
            GoBackCommand.OnCanExecuteChanged();
        }

        private void MenuItemInvoked()        
            => _navigationService.Navigate(SelectedMenuItem.TargetPageType);        
    }
}
