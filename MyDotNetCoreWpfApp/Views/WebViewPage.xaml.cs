using System.Windows.Controls;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Views
{
    public partial class WebViewPage : Page
    {
        private readonly WebViewViewModel _viewModel;

        public WebViewPage(WebViewViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            viewModel.Initialize(webView);
            DataContext = _viewModel;
        }

        private void OnNavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
            => _viewModel.OnNavigationCompleted(e);
    }
}