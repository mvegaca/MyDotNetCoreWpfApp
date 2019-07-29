using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public class NavigationService
    {
        private bool _isNavigated = false;
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        private Frame _frame;
        private object _lastExtraDataUsed;

        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        public bool CanGoBack => _frame != null && _frame.CanGoBack;

        public NavigationService()
        {
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

        public void Configure(string key, Type pageType)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(key))
                {
                    throw new ArgumentException($"The key {key} is already configured in NavigationService");
                }

                if (_pages.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageType).Key}");
                }

                _pages.Add(key, pageType);
            }
        }
        public void GoBack()
            => _frame.GoBack();

        public bool Navigate(string pageKey, object extraData = null)
        {
            Type pageType;
            lock (_pages)
            {
                if (!_pages.TryGetValue(pageKey, out pageType))
                {
                    throw new ArgumentException($"Page not found: {pageKey}. Did you forget to call NavigationService.Configure?");
                }
            }

            if (_frame.Content?.GetType() != pageType || (extraData != null && !extraData.Equals(_lastExtraDataUsed)))
            {
                var page = SimpleIoc.Default.GetInstance(pageType);
                var navigated = _frame.Navigate(page, extraData);
                if (navigated)
                {
                    _lastExtraDataUsed = extraData;
                    _isNavigated = true;
                }

                return navigated;
            }

            return false;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
            => Navigated?.Invoke(this, e);

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => NavigationFailed?.Invoke(this, e);
    }
}
