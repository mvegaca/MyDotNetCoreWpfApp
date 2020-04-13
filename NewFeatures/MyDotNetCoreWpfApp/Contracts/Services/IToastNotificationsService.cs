using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IToastNotificationsService
    {
        void ShowToastNotification(ToastNotification toastNotification);

        void ShowToastNotificationSample();
    }
}
