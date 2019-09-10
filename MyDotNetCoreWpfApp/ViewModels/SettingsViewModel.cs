using System;
using System.Windows.Input;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SettingsViewModel : Observable
    {
        private bool _isLightThemeSelected;
        private bool _isDarkThemeSelected;
        private string _selectedTheme;
        private string _versionDescription;
        private INavigationService _navigationService;
        private IThemeSelectorService _themeSelectorService;
        private ICommand _setThemeCommand;

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new RelayCommand<string>(OnSetTheme));

        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                Set(ref _selectedTheme, value);
                IsLightThemeSelected = value == Constants.ThemeLight;
                IsDarkThemeSelected = value == Constants.ThemeDark;
            }
        }

        public bool IsLightThemeSelected
        {
            get { return _isLightThemeSelected; }
            set { Set(ref _isLightThemeSelected, value); }
        }

        public bool IsDarkThemeSelected
        {
            get { return _isDarkThemeSelected; }
            set { Set(ref _isDarkThemeSelected, value); }
        }

        public string VersionDescription
        {
            get { return _versionDescription; }
            set { Set(ref _versionDescription, value); }
        }

        public SettingsViewModel(INavigationService navigationService, IThemeSelectorService themeSelectorService)
        {
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
            VersionDescription = GetVersionDescription();
            SelectedTheme = _themeSelectorService.GetCurrentThemeName();
        }

        private string GetVersionDescription()
        {
            var appName = "MyDotNetCoreWpfApp";
            return $"{appName} - {1}.{0}.{0}.{0}";
        }

        private void OnSetTheme(string themeName)
            => _themeSelectorService.SetTheme(themeName);

        //private void OnNavigated(object sender, NavigationEventArgs e)
        //{
        //    if (e.IsFromViewModel())
        //    {

        //    }
        //}
    }
}
