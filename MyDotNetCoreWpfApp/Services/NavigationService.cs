using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    public class NavigationService
    {
        private bool _isNavigated = false;
        private IServiceProvider _serviceProvider;
        private ShelWindow _shellWindow;
        private HamburgerMenu _hamburgerMenu;
        private HamburgerMenuItemCollection _menuItems;
        private Frame _frame;
        private Button _goBackButton;
        private ShelWindowViewModel ShelViewModel => _shellWindow.DataContext as ShelWindowViewModel;

        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        public bool CanGoBack => _frame.CanGoBack;

        public NavigationService(IServiceProvider serviceProvider, ShelWindow shellWindow)
        {
            _serviceProvider = serviceProvider;
            _shellWindow = shellWindow;
            _hamburgerMenu = shellWindow.Content as HamburgerMenu;
            _menuItems = _hamburgerMenu.ItemsSource as HamburgerMenuItemCollection;
            _frame = _hamburgerMenu.Content as Frame;
            _goBackButton = _shellWindow.LeftWindowCommands.Items.GetItemAt(0) as Button;
            _goBackButton.Command = new RelayCommand(GoBack, () => CanGoBack);

            _hamburgerMenu.ItemInvoked += OnMenuItemInvoked;
            _frame.Navigated += OnNavigated;
            _frame.NavigationFailed += OnNavigationFailed;
        }

        public bool IsNavigated()
            => _isNavigated;

        public void Show()
            => _shellWindow.Show();

        public bool Navigate<T>()
            where T : Page
            => Navigate(typeof(T));

        public bool Navigate<T>(object extraData)
            where T : Page
            => Navigate(typeof(T), extraData);

        public bool Navigate(Type pageType)
            => Navigate(_serviceProvider.GetService(pageType));

        public bool Navigate(Type pageType, object extraData)
            =>  Navigate(_serviceProvider.GetService(pageType), extraData);

        public bool Navigate(object content)
        {
            var navigated = _frame.Navigate(content);
            if (navigated)
            {
                _isNavigated = true;
            }

            return navigated;
        }

        public bool Navigate(object content, object extraData)
        {
            var navigated = _frame.Navigate(content, extraData);
            if (navigated)
            {
                _isNavigated = true;
            }

            return navigated;
        }

        public void GoBack()
            => _frame.GoBack();

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            var pageType = e.Content.GetType();
            var item = _menuItems
                .OfType<HamburgerMenuItem>()
                .First(i => i.TargetPageType == pageType);
            _hamburgerMenu.SelectedItem = item;
            ((RelayCommand)_goBackButton.Command).OnCanExecuteChanged();
            Navigated?.Invoke(this, e);
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, e);

        private void OnMenuItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            if (args.InvokedItem is HamburgerMenuItem item)
            {
                Navigate(item.TargetPageType);
            }
        }
    }
}