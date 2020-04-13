using System;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Extensions.Hosting;

namespace MyDotNetCoreWpfApp
{
    // The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("50cfb67f-bc8a-477d-938c-93cf6bfb3320"), ComVisible(true)]
    public class ToastNotificationActivator : NotificationActivator
    {
        private IHost _host
            => ((App)App.Current).ApplicationHost;

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
                }
                else
                {
                    await _host.StartAsync();
                }
            });
        }
    }
}
