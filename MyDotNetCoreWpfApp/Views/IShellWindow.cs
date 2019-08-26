using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();
    }
}
