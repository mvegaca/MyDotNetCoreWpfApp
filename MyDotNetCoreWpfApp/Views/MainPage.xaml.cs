using System.Windows.Controls;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel, PersistAndRestoreService persistAndRestoreService)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
            persistAndRestoreService.OnPersistData += OnPersistData;
        }

        private void OnPersistData(object sender, PersistAndRestoreArgs e)
        {
            e.PersistAndRestoreData.Data = _viewModel.Data;
        }
    }
}
