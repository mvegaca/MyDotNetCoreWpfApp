using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfMvvmLightApp.Services;

namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class ShellViewModel : ViewModelBase
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
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", Tag = typeof(MainViewModel).FullName },
            new HamburgerMenuGlyphItem() { Label = "Secondary", Glyph = "\uE8A5", Tag = typeof(SecondaryViewModel).FullName }
        };

        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(MenuItemInvoked));

        public ShellViewModel(NavigationService navigationService)
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
            var page = e.Content as FrameworkElement;
            var viewmodelName = page.DataContext;
            SelectedMenuItem = MenuItems
                                .OfType<HamburgerMenuItem>()
                                .First(i => i.Tag.ToString() == viewmodelName.GetType().FullName);
            GoBackCommand.RaiseCanExecuteChanged();
        }

        private void MenuItemInvoked()
            => _navigationService.Navigate(SelectedMenuItem.Tag.ToString());
    }
}
