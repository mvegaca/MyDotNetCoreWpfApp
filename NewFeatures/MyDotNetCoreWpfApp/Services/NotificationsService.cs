using System;
using System.Diagnostics;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Notifications;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Services
{
    public class NotificationsService : INotificationsService
    {
        public const string TOAST_ACTIVATED_LAUNCH_ARG = "-ToastActivated";

        private bool _registeredAumidAndComServer;
        private string _aumid;
        private bool _registeredActivator;

        public bool CanUseHttpImages
            => DesktopBridgeHelpers.IsRunningAsUwp();

        public DesktopNotificationHistoryCompat History
        {
            get
            {
                EnsureRegistered();
                return new DesktopNotificationHistoryCompat(_aumid);
            }
        }

        public NotificationsService()
        {
        }

        public ToastNotifier CreateToastNotifier()
        {
            EnsureRegistered();

            if (_aumid != null)
            {
                // Non-Desktop Bridge
                return ToastNotificationManager.CreateToastNotifier(_aumid);
            }
            else
            {
                // Desktop Bridge
                return ToastNotificationManager.CreateToastNotifier();
            }
        }

        public void RegisterActivator<T>() where T : NotificationActivator
        {
            // Register type
            var regService = new RegistrationServices();

            regService.RegisterTypeForComClients(
                typeof(T),
                RegistrationClassContext.LocalServer,
                RegistrationConnectionType.MultipleUse);

            _registeredActivator = true;
        }

        public void RegisterAumidAndComServer<T>(string aumid) where T : NotificationActivator
        {
            if (string.IsNullOrWhiteSpace(aumid))
            {
                throw new ArgumentException("You must provide an AUMID.", nameof(aumid));
            }

            // If running as Desktop Bridge
            if (DesktopBridgeHelpers.IsRunningAsUwp())
            {
                // Clear the AUMID since Desktop Bridge doesn't use it, and then we're done.
                // Desktop Bridge apps are registered with platform through their manifest.
                // Their LocalServer32 key is also registered through their manifest.
                _aumid = null;
                _registeredAumidAndComServer = true;
                return;
            }

            _aumid = aumid;

            var exePath = Process.GetCurrentProcess().MainModule.FileName;
            RegisterComServer<T>(exePath);

            _registeredAumidAndComServer = true;
        }

        private void RegisterComServer<T>(String exePath)
            where T : NotificationActivator
        {
            // We register the EXE to start up when the notification is activated
            string regString = string.Format("SOFTWARE\\Classes\\CLSID\\{{{0}}}\\LocalServer32", typeof(T).GUID);
            var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(regString);

            // Include a flag so we know this was a toast activation and should wait for COM to process
            // We also wrap EXE path in quotes for extra security
            key.SetValue(null, '"' + exePath + '"' + " " + TOAST_ACTIVATED_LAUNCH_ARG);
        }

        private void EnsureRegistered()
        {
            // If not registered AUMID yet
            if (!_registeredAumidAndComServer)
            {
                // Check if Desktop Bridge
                if (DesktopBridgeHelpers.IsRunningAsUwp())
                {
                    // Implicitly registered, all good!
                    _registeredAumidAndComServer = true;
                }

                else
                {
                    // Otherwise, incorrect usage
                    throw new Exception("You must call RegisterAumidAndComServer first.");
                }
            }

            // If not registered activator yet
            if (!_registeredActivator)
            {
                // Incorrect usage
                throw new Exception("You must call RegisterActivator first.");
            }
        }
    }
}
