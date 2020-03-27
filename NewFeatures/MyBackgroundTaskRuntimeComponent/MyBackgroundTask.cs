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
            ShowToast("Hello from BackgroundTask");
            //// TODO WTS: Insert the code that should be executed in the background task here.
            //// This sample initializes a timer that counts to 100 in steps of 10.  It updates Message each time.

            //// Documentation:
            ////      * General: https://docs.microsoft.com/windows/uwp/launch-resume/support-your-app-with-background-tasks
            ////      * Debug: https://docs.microsoft.com/windows/uwp/launch-resume/debug-a-background-task
            ////      * Monitoring: https://docs.microsoft.com/windows/uwp/launch-resume/monitor-background-task-progress-and-completion

            //// To show the background progress and message on any page in the application,
            //// subscribe to the Progress and Completed events.
            //// You can do this via "BackgroundTaskService.GetBackgroundTasksRegistration"

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
