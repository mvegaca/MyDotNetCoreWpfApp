using System;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

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
                await ((App)Application.Current).ActivateAsync(new string[] { arguments });
            });
        }
    }
}
