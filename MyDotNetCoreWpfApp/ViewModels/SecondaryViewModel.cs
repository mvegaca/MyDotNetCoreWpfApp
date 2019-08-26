using System;
using System.Windows.Input;
using System.Windows.Navigation;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SecondaryViewModel : Observable
    {
        private ICommand _goBackCommand;
        private INavigationService _navigationService;
        private string _navigationExtraData;

        public string NavigationExtraData
        {
            get { return _navigationExtraData; }
            set { Set(ref _navigationExtraData, value); }
        }

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public SecondaryViewModel(INavigationService navigationService, IPersistAndRestoreService persistAndRestoreService)
        {
            _navigationService = navigationService;
            _navigationService.Navigated += OnNavigated;
            persistAndRestoreService.OnPersistData += OnPersistData;
        }

        public void LoadData(string extraData)
        {
            NavigationExtraData = extraData;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.IsFromViewModel())
            {
                _navigationService.Navigated -= OnNavigated;
                if (e.ExtraData is PersistAndRestoreData restoreData)
                {
                    LoadData(restoreData.Data.ToString());
                }
                else
                {
                    LoadData(e.ExtraData?.ToString());
                }
            }
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            =>_navigationService.GoBack();

        private void OnPersistData(object sender, PersistAndRestoreArgs e)
        {
            e.PersistAndRestoreData.Data = "Data restored!";
        }
    }
}
