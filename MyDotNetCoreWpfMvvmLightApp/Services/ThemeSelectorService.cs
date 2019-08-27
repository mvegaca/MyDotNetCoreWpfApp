using System.Windows;
using MahApps.Metro;
using Microsoft.Win32;
using MyDotNetCoreWpfApp.Core.Helpers;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private bool _isHighContrastActive
                        => SystemParameters.HighContrast;

        public ThemeSelectorService()
        {
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        public void SetTheme(string themeName = null)
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
                    themeName = Themes.BaseLightTheme;
                }
            }

            var currentTheme = ThemeManager.DetectTheme(Application.Current);
            if (currentTheme == null || currentTheme.Name != themeName)
            {
                ThemeManager.ChangeTheme(Application.Current, themeName);
                App.Current.Properties["Theme"] = themeName.ToString();
            }
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