using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable
    {
        private NavigationService _navigationService;
        private ThemeSelectorService _themeSelectorService;
        private ICommand _navigateCommand;
        private ICommand _setLightThemeCommand;
        private ICommand _setDarkThemeCommand;

        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new RelayCommand(OnNavigate));

        public ICommand SetLightThemeCommand => _setLightThemeCommand ?? (_setLightThemeCommand = new RelayCommand(OnSetLightTheme));

        public ICommand SetDarkThemeCommand => _setDarkThemeCommand ?? (_setDarkThemeCommand = new RelayCommand(OnSetDarkTheme));

        public MainViewModel(NavigationService navigationService, ThemeSelectorService themeSelectorService)
        {
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
        }

        private void OnNavigate()
        {
            _navigationService.Navigate<SecondaryPage>("Hello world!");
        }

        private void OnSetLightTheme()
            => _themeSelectorService.SetTheme(ThemeSelectorService.LightTheme);

        private void OnSetDarkTheme()
            => _themeSelectorService.SetTheme(ThemeSelectorService.DarkTheme);
    }
}
