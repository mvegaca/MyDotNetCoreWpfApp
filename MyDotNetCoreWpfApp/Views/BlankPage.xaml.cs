using System.Windows.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    public partial class BlankPage : Page
    {
        public BlankPage(BlankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
