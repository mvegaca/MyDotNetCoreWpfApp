using System.Windows.Input;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SettingsViewModel : Observable, INavigationAware
    {
        private bool _isLightThemeSelected;
        private bool _isDarkThemeSelected;
        private string _versionDescription;
        private IThemeSelectorService _themeSelectorService;
        private ICommand _setThemeCommand;

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new RelayCommand<string>(OnSetTheme));

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

        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;
        }

        public void OnNavigatedTo(object ExtraData)
        {
            VersionDescription = GetVersionDescription();
            IsLightThemeSelected = _themeSelectorService.IsLightThemeSelected();
            IsDarkThemeSelected = _themeSelectorService.IsDarkThemeSelected();
        }

        public void OnNavigatingFrom()
        {
        }

        private string GetVersionDescription()
        {
            var appName = "MyDotNetCoreWpfApp";
            return $"{appName} - {1}.{0}.{0}.{0}";
        }

        private void OnSetTheme(string themeName)
            => _themeSelectorService.SetTheme(themeName);
    }
}
