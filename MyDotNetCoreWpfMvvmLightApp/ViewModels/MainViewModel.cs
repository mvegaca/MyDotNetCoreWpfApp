using System;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfMvvmLightApp.Services;

namespace MyDotNetCoreWpfMvvmLightApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IThemeSelectorService _themeSelectorService;
        private int _data;
        private ICommand _navigateCommand;
        private ICommand _setLightThemeCommand;
        private ICommand _setDarkThemeCommand;
        private DispatcherTimer _timer;

        public int Data
        {
            get { return _data; }
            set { Set(ref _data, value); }
        }

        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new RelayCommand(OnNavigate));

        public ICommand SetLightThemeCommand => _setLightThemeCommand ?? (_setLightThemeCommand = new RelayCommand(OnSetLightTheme));

        public ICommand SetDarkThemeCommand => _setDarkThemeCommand ?? (_setDarkThemeCommand = new RelayCommand(OnSetDarkTheme));

        public MainViewModel(INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService)
        {
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _navigationService.Navigated += OnNavigated;
            _navigationService.Navigating += OnNavigating;
            persistAndRestoreService.OnPersistData += OnPersistData;
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
        }

        private void OnPersistData(object sender, PersistAndRestoreArgs e)
        {
            e.PersistAndRestoreData.Data = Data;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Data = Data + 1;
        }        

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.IsFromViewModel())
            {
                if (e.ExtraData is PersistAndRestoreData restoreData)
                {
                    Data = int.Parse(restoreData.Data.ToString());
                }

                _timer.Start();
            }
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (!e.IsFromThisViewModel())
            {
                _timer.Stop();
            }
        }

        private void OnNavigate()
            => _navigationService.Navigate(typeof(SecondaryViewModel).FullName, "Navigation data as navigation parameter!");

        private void OnSetLightTheme()
            => _themeSelectorService.SetTheme(Themes.BaseLightTheme);

        private void OnSetDarkTheme()
            => _themeSelectorService.SetTheme(Themes.BaseDarkTheme);
    }
}
