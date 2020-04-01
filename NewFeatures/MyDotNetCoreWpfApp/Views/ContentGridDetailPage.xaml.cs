using System.Windows.Controls;

using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    public partial class ContentGridDetailPage : Page
    {
        public ContentGridDetailPage(ContentGridDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
