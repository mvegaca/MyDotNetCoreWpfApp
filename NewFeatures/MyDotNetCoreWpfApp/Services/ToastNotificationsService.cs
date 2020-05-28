using Microsoft.Toolkit.Uwp.Notifications;
using MyDotNetCoreWpfApp.Contracts.Services;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Services
{
    public partial class ToastNotificationsService : IToastNotificationsService
    {
        public ToastNotificationsService()
        {
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
        }
    }
}