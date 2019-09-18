using System;
using System.Diagnostics;
using MyDotNetCoreWpfAppPrism.Contracts.Services;
using MyDotNetCoreWpfAppPrism.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace MyDotNetCoreWpfAppPrism.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private AppTheme _theme;
        private string _versionDescription;
        private IThemeSelectorService _themeSelectorService;

        public DelegateCommand<string> SetThemeCommand { get; private set; }
        public DelegateCommand PrivacyStatementCommand { get; private set; }

        public AppTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        public string VersionDescription
        {
            get { return _versionDescription; }
            set { SetProperty(ref _versionDescription, value); }
        }

        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;
            SetThemeCommand = new DelegateCommand<string>(OnSetTheme);
            PrivacyStatementCommand = new DelegateCommand(OnPrivacyStatement);
            GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var appName = "MyDotNetCoreWpfAppPrism";
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