using MyDotNetCoreWpfApp.Contracts.Activation;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IToastNotificationsService : IActivationHandler
    {
        void ShowToastNotification(ToastNotification toastNotification);

        void ShowToastNotificationSample();
    }
}
