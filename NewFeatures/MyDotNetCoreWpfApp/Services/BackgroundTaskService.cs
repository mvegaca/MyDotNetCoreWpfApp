using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Contracts.Services;
using Windows.ApplicationModel.Background;

namespace MyDotNetCoreWpfApp.Services
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        public async Task RegisterBackbroundTasksAsync()
        {
            BackgroundExecutionManager.RemoveAccess();
            var result = await BackgroundExecutionManager.RequestAccessAsync();

            if (result == BackgroundAccessStatus.DeniedBySystemPolicy
                || result == BackgroundAccessStatus.DeniedByUser)
            {
                return;
            }

            foreach (var taskBuilder in GetInstances())
            {
                taskBuilder.Register();
            }
        }

        private IEnumerable<BackgroundTaskBuilder> GetInstances()
        {
            var instances = new List<BackgroundTaskBuilder>();

            var myBackgroundTaskBuilder = new BackgroundTaskBuilder();
            myBackgroundTaskBuilder.Name = "MyBackgroundTask";
            myBackgroundTaskBuilder.SetTrigger(new MaintenanceTrigger(15, false));
            myBackgroundTaskBuilder.TaskEntryPoint = "MyBackgroundTaskRuntimeComponent.MyBackgroundTask";
            instances.Add(myBackgroundTaskBuilder);

            return instances;
        }
    }
}
