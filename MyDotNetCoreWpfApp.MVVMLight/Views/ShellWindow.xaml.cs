using System.Windows.Controls;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Views;

namespace MyDotNetCoreWpfApp.MVVMLight.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();
    }
}
