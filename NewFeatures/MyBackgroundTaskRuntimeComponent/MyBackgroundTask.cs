using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace MyBackgroundTaskRuntimeComponent
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += OnTaskCanceled;
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            //// TODO: Run here your background code
            ///
            deferral.Complete();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
        }
    }
}
