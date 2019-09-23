using System;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.ViewModels;
using MyDotNetCoreWpfApp.MVVMLight.Models;

namespace MyDotNetCoreWpfApp.MVVMLight.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationAware
    {
        private AppTheme _theme;
        private string _versionDescription;
        private IThemeSelectorService _themeSelectorService;
        private ICommand _setThemeCommand;
        private ICommand _privacyStatementCommand;

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new RelayCommand<string>(OnSetTheme));
        public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new RelayCommand(OnPrivacyStatement));

        public AppTheme Theme
        {
            get { return _theme; }
            set { Set(ref _theme, value); }
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

        public void OnNavigatedTo(object parameter)
        {
            VersionDescription = GetVersionDescription();
            Theme = _themeSelectorService.GetCurrentTheme();
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
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnPrivacyStatement()
        {
            Process.Start("https://YourPrivacyUrlGoesHere/");
        }
    }
}
