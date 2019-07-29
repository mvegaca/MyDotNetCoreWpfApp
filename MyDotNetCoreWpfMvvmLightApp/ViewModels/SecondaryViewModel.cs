using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyDotNetCoreWpfMvvmLightApp.Services;

namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class SecondaryViewModel : ViewModelBase
    {
        private ICommand _goBackCommand;
        private NavigationService _navigationService;
        private string _navigationExtraData;

        public string NavigationExtraData
        {
            get { return _navigationExtraData; }
            set { Set(ref _navigationExtraData, value); }
        }

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack));

        public SecondaryViewModel(NavigationService navigationService)
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                _navigationService = navigationService;
                _navigationService.Navigated += OnNavigated;
            }
        }

        public void LoadData(string extraData)
        {
            NavigationExtraData = extraData;
        }

        private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadData(e.ExtraData?.ToString());
            _navigationService.Navigated -= OnNavigated;
        }

        private void OnGoBack()
        {
            _navigationService.GoBack();
        }
    }
}
