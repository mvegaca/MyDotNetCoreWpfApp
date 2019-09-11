using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    public class NavigationService : INavigationService
    {
        private bool _isNavigated = false;
        private IServiceProvider _serviceProvider;
        private Frame _frame;
        private object _lastExtraDataUsed;
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        public event EventHandler<string> Navigated;

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

        public bool IsNavigated()
            => _isNavigated;

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
                var page = _serviceProvider.GetService(pageType);
                if (_frame.Content is FrameworkElement element)
                {
                    if (element.DataContext is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatingFrom();
                    }
                }
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
        {
            if (e.Content is FrameworkElement element)
            {
                if (element.DataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }

                Navigated?.Invoke(sender, element.DataContext.GetType().FullName);
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
        }
    }
}