using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    internal class NavigationService
    {
        private Frame _frame;

        internal event NavigatedEventHandler Navigated;

        internal event NavigationFailedEventHandler NavigationFailed;

        internal NavigationService()
        {
        }

        internal void Initialize(Frame frame)
        {
            _frame = frame;
            _frame.Navigated += OnNavigated;
            _frame.NavigationFailed += OnNavigationFailed;
        }

        internal bool Navigate<T>()
            where T : Page
            => Navigate(typeof(T));

        internal bool Navigate<T>(object extraData)
            where T : Page
            => Navigate(typeof(T), extraData);

        internal bool Navigate(Type pageType)
            => _frame.Navigate(Activator.CreateInstance(pageType));

        internal bool Navigate(Type pageType, object extraData)
            => _frame.Navigate(Activator.CreateInstance(pageType), extraData);

        internal bool Navigate(object content)
            => _frame.Navigate(content);

        internal bool Navigate(object content, object extraData)
            => _frame.Navigate(content, extraData);

        private void OnNavigated(object sender, NavigationEventArgs e)
            => Navigated?.Invoke(this, e);

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, e);
    }
}
