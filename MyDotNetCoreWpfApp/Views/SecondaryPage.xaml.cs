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
    /// Interaction logic for SecondaryPage.xaml
    /// </summary>
    public partial class SecondaryPage : Page
    {
        public SecondaryViewModel ViewModel { get; } = new SecondaryViewModel();

        public SecondaryPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            App.CurrentApp.NavigationService.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            ViewModel.LoadData(e.ExtraData?.ToString());
            App.CurrentApp.NavigationService.Navigated -= OnNavigated;
        }
    }
}
