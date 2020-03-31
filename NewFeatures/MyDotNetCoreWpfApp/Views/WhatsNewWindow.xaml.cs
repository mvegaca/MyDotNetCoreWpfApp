using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    public partial class WhatsNewWindow : MetroWindow
    {
        public WhatsNewWindow(WhatsNewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Initialize(this);
        }
    }
}
