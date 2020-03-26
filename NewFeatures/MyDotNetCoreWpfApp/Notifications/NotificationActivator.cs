using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Notifications
{
    public abstract class NotificationActivator : INotificationActivationCallback
    {
        public void Activate(string appUserModelId, string invokedArgs, NotificationUserInputData[] data, uint dataCount)
        {
            OnActivated(invokedArgs, new NotificationUserInput(data), appUserModelId);
        }
        
        public abstract void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId);
    }
}
