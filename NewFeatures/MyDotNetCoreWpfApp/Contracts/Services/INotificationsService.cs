using MyDotNetCoreWpfApp.Notifications;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface INotificationsService
    {
        bool CanUseHttpImages { get; }

        DesktopNotificationHistoryCompat History { get; }

        void RegisterAumidAndComServer<T>(string aumid)
            where T : NotificationActivator;

        void RegisterActivator<T>()
            where T : NotificationActivator;

        ToastNotifier CreateToastNotifier();
    }
}
