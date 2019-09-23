using System;
using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface INavigationService
    {
        event EventHandler<string> Navigated;

        bool CanGoBack { get; }

        void Initialize(Frame shellFrame);
        
        bool Navigate(string pageKey, object parameter = null, bool clearNavigation = false);

        void GoBack();
    }
}
