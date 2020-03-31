using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    public partial class FirstRunWindow : MetroWindow
    {
        public FirstRunWindow(FirstRunViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Initialize(this);
        }
    }
}
