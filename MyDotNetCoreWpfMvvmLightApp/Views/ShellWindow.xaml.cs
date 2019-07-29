using MahApps.Metro.Controls;
using MyDotNetCoreWpfMvvmLightApp.Services;

namespace MyDotNetCoreWpfMvvmLightApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(NavigationService navigationService)
        {
            InitializeComponent();
            navigationService.Initialize(shellFrame);
        }
    }
}
