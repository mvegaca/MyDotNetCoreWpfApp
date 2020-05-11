using System;
using System.Runtime.InteropServices;
using System.Windows;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Notifications
{
    // The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("50cfb67f-bc8a-477d-938c-93cf6bfb3320"), ComVisible(true)]
    public class ToastNotificationActivator : NotificationActivator
    {
        private bool IsApplicationStarted
            => App.Current.Windows.Count > 0;

        public override async void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId)
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {                

                if (IsApplicationStarted)
                {
                    App.Current.MainWindow.Activate();
                    if (App.Current.MainWindow.WindowState == WindowState.Minimized)
                    {
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                    }

                    if (arguments == "ToastContentActivationParams")
                    {
                        var navigationService = ((App)Application.Current).Services.GetService(typeof(INavigationService)) as INavigationService;
                        navigationService.NavigateTo(typeof(SettingsViewModel).FullName);
                    }
                }
                else
                {
                    if (arguments == "ToastContentActivationParams")
                    {
                        var pageService = ((App)Application.Current).Services.GetService(typeof(IPageService)) as IPageService;
                        pageService.ConfigureDefaultNavigation(typeof(SettingsViewModel).FullName);
                    }

                    await ((App)Application.Current).StartAsync();
                }
            });
        }
    }
}
