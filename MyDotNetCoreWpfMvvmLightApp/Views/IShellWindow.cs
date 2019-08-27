using System.Windows.Controls;

namespace MyDotNetCoreWpfMvvmLightApp.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();
    }
}
