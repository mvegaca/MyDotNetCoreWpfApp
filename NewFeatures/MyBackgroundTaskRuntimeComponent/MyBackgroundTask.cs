using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace MyBackgroundTaskRuntimeComponent
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += OnTaskCanceled;
            var deferral = taskInstance.GetDeferral();
            //// TODO WTS: Insert the code that should be executed in the background task here.
            //// Uncomment the following line to show a toast notification from this background task.
            // ShowToast("Hello from BackgroundTask");

            deferral.Complete();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // TODO WTS: Insert code to handle the cancelation request here.
            // Documentation: https://docs.microsoft.com/windows/uwp/launch-resume/handle-a-cancelled-background-task
        }

        private void ShowToast(string msg)
        {
            var toastTemplate = ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
