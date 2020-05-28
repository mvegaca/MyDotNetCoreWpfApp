using MyDotNetCoreWpfApp.Contracts.Activation;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IToastNotificationsService
    {
        public abstract void ShowToastNotification(ToastNotification toastNotification);

        public abstract void ShowToastNotificationSample();
    }
}
