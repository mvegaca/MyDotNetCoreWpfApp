using System.Windows.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MasterDetailPage.xaml
    /// </summary>
    public partial class MasterDetailPage : Page
    {
        public MasterDetailPage(MasterDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
