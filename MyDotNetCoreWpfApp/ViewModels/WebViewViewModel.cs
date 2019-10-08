using System.Windows;
using System.Windows.Input;
using CommunityToolkit = Microsoft.Toolkit.Wpf.UI.Controls;
using MyDotNetCoreWpfApp.Helpers;
using System.Diagnostics;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class WebViewViewModel : Observable
    {
        // TODO WTS: Set the URI of the page to show by default
        private const string DefaultUrl = "https://developer.microsoft.com/en-us/windows/apps";

        private string _source;
        private bool _isLoading = true;
        private bool _isShowingFailedMessage = false;
        private Visibility _isLoadingVisibility = Visibility.Visible;
        private Visibility _failedMesageVisibility = Visibility.Collapsed;
        private ICommand _refreshCommand;
        private RelayCommand _browserBackCommand;
        private RelayCommand _browserForwardCommand;
        private ICommand _openInBrowserCommand;

        private CommunityToolkit.WebView _webView;

        public string Source
        {
            get { return _source; }
            set { Set(ref _source, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }

            set
            {
                Set(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsShowingFailedMessage
        {
            get { return _isShowingFailedMessage; }

            set
            {
                Set(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsLoadingVisibility
        {
            get { return _isLoadingVisibility; }
            set { Set(ref _isLoadingVisibility, value); }
        }

        public Visibility FailedMesageVisibility
        {
            get { return _failedMesageVisibility; }
            set { Set(ref _failedMesageVisibility, value); }
        }

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(OnRefresh));

        public RelayCommand BrowserBackCommand => _browserBackCommand ?? (_browserBackCommand = new RelayCommand(() => _webView?.GoBack(), () => _webView?.CanGoBack ?? false));

        public RelayCommand BrowserForwardCommand => _browserForwardCommand ?? (_browserForwardCommand = new RelayCommand(() => _webView?.GoForward(), () => _webView?.CanGoForward ?? false));

        public ICommand OpenInBrowserCommand => _openInBrowserCommand ?? (_openInBrowserCommand = new RelayCommand(OnOpenInBrowser));

        public WebViewViewModel()
        {
            Source = DefaultUrl;
        }

        public void Initialize(CommunityToolkit.WebView webView)
        {
            _webView = webView;
        }

        public void OnNavigationCompleted(WebViewControlNavigationCompletedEventArgs e)
        {
            IsLoading = false;
            BrowserBackCommand.OnCanExecuteChanged();
            BrowserForwardCommand.OnCanExecuteChanged();
            if (e != null && !e.IsSuccess)
            {
                // Use `args.WebErrorStatus` to vary the displayed message based on the error reason
                IsShowingFailedMessage = true;
            }
        }

        private void OnRefresh()
        {
            IsShowingFailedMessage = false;
            IsLoading = true;
            _webView?.Refresh();
        }

        private void OnOpenInBrowser()
        {
            // There is an open Issue on this
            // https://github.com/dotnet/corefx/issues/10361
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Source,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
