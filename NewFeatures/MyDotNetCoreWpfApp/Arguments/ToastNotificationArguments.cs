using Microsoft.Toolkit.Uwp.Notifications;

namespace MyDotNetCoreWpfApp.Arguments
{
    public class ToastNotificationArguments
    {
        public string Arguments { get; set; }
        
        public NotificationUserInput UserInput { get; set; }
        
        public string AppUserModelId { get; set; }

        public ToastNotificationArguments(string arguments, NotificationUserInput userInput, string appUserModelId)
        {
            Arguments = arguments;
            UserInput = userInput;
            AppUserModelId = appUserModelId;
        }
    }
}
