using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;

namespace MyDotNetCoreWpfApp.MVVMLight.Services
{
    public class NavigationService : INavigationService
    {
        public bool CanGoBack => throw new NotImplementedException();

        public string CurrentPageKey => throw new NotImplementedException();

        public event EventHandler<string> Navigated;

        public void Configure(string viewModelName, Type pageType)
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void Initialize(Frame shellFrame)
        {
            throw new NotImplementedException();
        }

        public bool IsNavigated()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
