using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();
    }
}