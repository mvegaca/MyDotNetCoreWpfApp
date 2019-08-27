using System.Diagnostics;

namespace System.Windows.Navigation
{
    public static class NavigationExtensions
    {
        public static bool IsFromViewModel(this NavigationEventArgs e, Type viewModelType = null)
            => IsObjectFromViewModel(e.Content, viewModelType);

        public static bool IsFromThisViewModel(this NavigatingCancelEventArgs e)
            => IsObjectFromViewModel(e.Content);

        private static bool IsObjectFromViewModel(object obj, Type viewModelType = null)
        {
            if (obj is FrameworkElement element)
            {
                if (viewModelType == null)
                {
                    var stackTrace = new StackTrace();
                    var frame = stackTrace.GetFrames()[2];
                    var method = frame.GetMethod();
                    viewModelType = method.DeclaringType;
                }
                
                return element.DataContext.GetType() == viewModelType;
            }

            return false;
        }
    }
}