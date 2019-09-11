using System.Windows.Controls;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class SecondaryPage : Page
    {
        public SecondaryPage(SecondaryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }        
    }
}
