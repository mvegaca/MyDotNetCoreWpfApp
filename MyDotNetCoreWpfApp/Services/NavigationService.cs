using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.ViewModels;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    public class NavigationService : INavigationService
    {
        private IServiceProvider _serviceProvider;
        private Frame _frame;
        private object _lastParameterUsed;
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
            }

            Configure(typeof(MainViewModel).FullName, typeof(MainPage));
            Configure(typeof(BlankViewModel).FullName, typeof(BlankPage));
            Configure(typeof(MasterDetailViewModel).FullName, typeof(MasterDetailPage));
            Configure(typeof(SettingsViewModel).FullName, typeof(SettingsPage));
        }

        private void Configure(string key, Type pageType)
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

        public bool Navigate(string pageKey, object parameter = null, bool clearNavigation = false)
        {
            Type pageType;
            lock (_pages)
            {
                if (!_pages.TryGetValue(pageKey, out pageType))
                {
                    throw new ArgumentException($"Page not found: {pageKey}. Did you forget to call NavigationService.Configure?");
                }
            }
            if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                var page = _serviceProvider.GetService(pageType);
                if (_frame.Content is FrameworkElement element)
                {
                    if (element.DataContext is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatingFrom();
                    }
                }
                _frame.Tag = clearNavigation;
                var navigated = _frame.Navigate(page, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                }

                return navigated;
            }

            return false;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                bool clearNavigation = (bool)frame.Tag;
                if (clearNavigation)
                {
                    while (frame.CanGoBack)
                    {
                        frame.RemoveBackEntry();
                    }
                }
            }

            if (e.Content is FrameworkElement element)
            {
                if (element.DataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }

                Navigated?.Invoke(sender, element.DataContext.GetType().FullName);
            }
        }
    }
}