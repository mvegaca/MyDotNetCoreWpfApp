using System.Windows;
using MahApps.Metro;
using Microsoft.Win32;
using MyDotNetCoreWpfApp.Core.Helpers;

namespace MyDotNetCoreWpfApp.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private bool _isHighContrastActive
                        => SystemParameters.HighContrast;

        public ThemeSelectorService()
        {
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        public bool SetTheme(string themeName = null)
        {
            if (_isHighContrastActive)
            {
                //TODO: Set high contrast theme name
            }
            else if (string.IsNullOrEmpty(themeName))
            {
                if (App.Current.Properties.Contains("Theme"))
                {
                    // Saved theme
                    themeName = App.Current.Properties["Theme"]?.ToString();
                }
                else
                {
                    // Default theme
                    themeName = Constants.ThemeLight;
                }
            }

            var currentTheme = ThemeManager.DetectTheme(Application.Current);
            if (currentTheme == null || currentTheme.Name != themeName)
            {
                ThemeManager.ChangeTheme(Application.Current, themeName);
                App.Current.Properties["Theme"] = themeName.ToString();
                return true;
            }

            return false;
        }

        public string GetCurrentThemeName()
        {
            var themeName = App.Current.Properties["Theme"]?.ToString();
            return !string.IsNullOrEmpty(themeName) ? themeName : Constants.ThemeLight;
        }

        private void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Color ||
                e.Category == UserPreferenceCategory.VisualStyle)
            {
                SetTheme();
            }
        }
    }
}