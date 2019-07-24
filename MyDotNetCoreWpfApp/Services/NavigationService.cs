using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    public class NavigationService
    {
        private bool _isNavigated = false;
        private IServiceProvider _serviceProvider;
        private Frame _frame;
        private object _lastExtraDataUsed;

        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        public bool CanGoBack => _frame.CanGoBack;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize(Frame shellFrame)
        {
            if (_frame == null)
            {
                _frame = shellFrame;
                _frame.Navigated += OnNavigated;
                _frame.NavigationFailed += OnNavigationFailed;
            }
        }

        public bool IsNavigated()
            => _isNavigated;

        public bool Navigate<T>()
            where T : Page
            => Navigate(typeof(T));

        public bool Navigate<T>(object extraData)
            where T : Page
            => Navigate(typeof(T), extraData);

        public bool Navigate(Type pageType, object extraData = null)
        {
            if (_frame.Content?.GetType() != pageType || (extraData != null && !extraData.Equals(_lastExtraDataUsed)))
            {
                return Navigate(_serviceProvider.GetService(pageType), extraData);
            }

            return false;
        }

        public bool Navigate(object content, object extraData)
        {
            var navigated = _frame.Navigate(content, extraData);
            if (navigated)
            {
                _lastExtraDataUsed = extraData;
                _isNavigated = true;
            }

            return navigated;
        }

        public void GoBack()
            => _frame.GoBack();

        private void OnNavigated(object sender, NavigationEventArgs e)
            => Navigated?.Invoke(this, e);

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, e);
    }
}