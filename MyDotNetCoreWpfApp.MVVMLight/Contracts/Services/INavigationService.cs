using System;
using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.Services
{
    public interface INavigationService : GalaSoft.MvvmLight.Views.INavigationService
    {
        event EventHandler<string> Navigated;

        bool CanGoBack { get; }

        void Initialize(Frame shellFrame);

        void Configure(string viewModelName, Type pageType);

        void NavigateTo(string pageKey, object parameter, bool clearNavigation);
    }
}