using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
