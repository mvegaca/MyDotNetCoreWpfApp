using System;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Uwp.Notifications;

namespace MyDotNetCoreWpfApp.Notifications
{
    // The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("50cfb67f-bc8a-477d-938c-93cf6bfb3320"), ComVisible(true)]
    public class ToastNotificationActivator : NotificationActivator
    {
        public override async void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId)
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                var app = Application.Current as App;
                var config = app.GetService<IConfiguration>();
                config[App.ToastNotificationArgs] = arguments;
                await app.StartAsync();
            });
        }
    }
}
