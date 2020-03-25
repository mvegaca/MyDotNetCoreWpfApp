using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.BackgroundTasks;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Helpers;
using Windows.ApplicationModel.Background;

namespace MyDotNetCoreWpfApp.Services
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        public IEnumerable<BackgroundTask> BackgroundTasks => BackgroundTaskInstances.Value;

        private readonly Lazy<IEnumerable<BackgroundTask>> BackgroundTaskInstances =
            new Lazy<IEnumerable<BackgroundTask>>(() => CreateInstances());

        public BackgroundTaskService()
        {
        }

        public async Task RegisterBackgroundTasksAsync()
        {
            BackgroundExecutionManager.RemoveAccess();
            var result = await BackgroundExecutionManager.RequestAccessAsync();

            if (result == BackgroundAccessStatus.DeniedBySystemPolicy
                || result == BackgroundAccessStatus.DeniedByUser)
            {
                return;
            }

            foreach (var task in BackgroundTasks)
            {
                task.Register();
            }
        }

        public void Start(IBackgroundTaskInstance taskInstance)
        {
            var task = BackgroundTasks.FirstOrDefault(b => b.Match(taskInstance?.Task?.Name));

            if (task == null)
            {
                // This condition should not be met. It is it it means the background task to start was not found in the background tasks managed by this service.
                // Please check CreateInstances to see if the background task was properly added to the BackgroundTasks property.
                return;
            }

            task.RunAsync(taskInstance).FireAndForget();
        }

        public BackgroundTaskRegistration GetBackgroundTasksRegistration<T>()
            where T : BackgroundTask
        {
            if (!BackgroundTaskRegistration.AllTasks.Any(t => t.Value.Name == typeof(T).Name))
            {
                // This condition should not be met. If it is it means the background task was not registered correctly.
                // Please check CreateInstances to see if the background task was properly added to the BackgroundTasks property.
                return null;
            }

            return (BackgroundTaskRegistration)BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == typeof(T).Name).Value;
        }

        private static IEnumerable<BackgroundTask> CreateInstances()
        {
            var backgroundTasks = new List<BackgroundTask>();

            backgroundTasks.Add(new MyBackgroundTask());
            return backgroundTasks;
        }
    }
}
