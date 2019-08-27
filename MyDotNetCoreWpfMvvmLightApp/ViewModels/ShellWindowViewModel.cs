using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfMvvmLightApp.Services;

namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class ShellWindowViewModel : ViewModelBase, IDisposable
    {
        private INavigationService _navigationService;
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
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", TargetPageType = typeof(MainViewModel) },
            new HamburgerMenuGlyphItem() { Label = "Secondary", Glyph = "\uE8A5", TargetPageType = typeof(SecondaryViewModel) }
        };

        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(MenuItemInvoked));

        public ShellWindowViewModel(INavigationService navigationService)
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

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            //var page = e.Content as FrameworkElement;
            //var viewModelType = page.DataContext.GetType();
            SelectedMenuItem = MenuItems
                                .OfType<HamburgerMenuItem>()
                                .First(i => e.IsFromViewModel(i.TargetPageType));
            GoBackCommand.RaiseCanExecuteChanged();
        }

        private void MenuItemInvoked()        
            => _navigationService.Navigate(SelectedMenuItem.TargetPageType.FullName);        
    }
}
