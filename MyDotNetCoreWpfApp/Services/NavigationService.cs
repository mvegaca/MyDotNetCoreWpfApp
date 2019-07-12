using MyDotNetCoreWpfApp.Views;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    public class NavigationService
    {
        private IServiceProvider _serviceProvider;
        private ShelWindow _shellWindow;
        private Frame _frame;

        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        public bool CanGoBack => _frame.CanGoBack;

        public NavigationService(IServiceProvider serviceProvider, ShelWindow shellWindow)
        {
            _serviceProvider = serviceProvider;
            _shellWindow = shellWindow;
            _frame = shellWindow.Content as Frame;
            _frame.Navigated += OnNavigated;
            _frame.NavigationFailed += OnNavigationFailed;
        }

        public bool IsNavigated()
            => _frame.Content != null;

        public void Show()
            => _shellWindow.Show();

        public bool Navigate<T>()
            where T : Page
            => Navigate(typeof(T));

        public bool Navigate<T>(object extraData)
            where T : Page
            => Navigate(typeof(T), extraData);

        public bool Navigate(Type pageType)
            => _frame.Navigate(_serviceProvider.GetService(pageType));

        public bool Navigate(Type pageType, object extraData)
            => _frame.Navigate(_serviceProvider.GetService(pageType), extraData);

        public bool Navigate(object content)
            => _frame.Navigate(content);

        public bool Navigate(object content, object extraData)
            => _frame.Navigate(content, extraData);        

        public void GoBack()
            => _frame.GoBack();

        private void OnNavigated(object sender, NavigationEventArgs e)
            => Navigated?.Invoke(this, e);

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, e);
    }
}
