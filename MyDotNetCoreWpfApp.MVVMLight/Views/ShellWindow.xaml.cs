using System.Windows.Controls;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Views;

namespace MyDotNetCoreWpfApp.MVVMLight.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
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
