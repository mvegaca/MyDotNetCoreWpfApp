using System;
using System.Windows.Input;
using System.Windows.Threading;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable
    {
        private NavigationService _navigationService;
        private ThemeSelectorService _themeSelectorService;
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

        public MainViewModel(NavigationService navigationService, ThemeSelectorService themeSelectorService)
        {
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            _navigationService.Navigated += OnNavigated;            
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Data = Data + 1;
        }        

        private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.ExtraData is PersistAndRestoreData restoreData)
            {
                Data = int.Parse(restoreData.Data.ToString());
            }

            _timer.Start();
        }

        private void OnNavigate()
        {
            _navigationService.Navigate<SecondaryPage>("Hello world!");
        }

        private void OnSetLightTheme()
            => _themeSelectorService.SetTheme(ThemeSelectorService.BaseLightTheme);

        private void OnSetDarkTheme()
            => _themeSelectorService.SetTheme(ThemeSelectorService.BaseDarkTheme);
    }
}
