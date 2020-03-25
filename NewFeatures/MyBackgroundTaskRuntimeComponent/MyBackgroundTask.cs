using System;
using Windows.UI.Notifications;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using Windows.ApplicationModel.Background;

namespace MyBackgroundTaskRuntimeComponent
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        private readonly IToastNotificationService _toastNotificationService;

        public MyBackgroundTask()
        {
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += OnTaskCanceled;
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            //// TODO: Run here your background code
            ///
            ShowToast("Hello from MyBackgroundTask");
            deferral.Complete();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
        }
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
