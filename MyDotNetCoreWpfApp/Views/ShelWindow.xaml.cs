using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShelWindow : MetroWindow
    {
        public ShelWindow(ShelWindowViewModel viewModel, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = viewModel;
            navigationService.Initialize(shellFrame);
        }
    }
}
