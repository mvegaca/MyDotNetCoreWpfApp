using MyDotNetCoreWpfApp.Arguments;
using MyDotNetCoreWpfApp.Contracts.Activation;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public abstract class IToastNotificationsService : IActivationHandler<ToastNotificationArguments>
    {
        public abstract void ShowToastNotification(ToastNotification toastNotification);

        public abstract void ShowToastNotificationSample();
    }
}
