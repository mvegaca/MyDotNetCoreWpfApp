using System;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Services
{
    public class ToastNotificationService : IToastNotificationService
    {
        public void ShowToast(string message)
        {
            var toastTemplate = ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));

            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
