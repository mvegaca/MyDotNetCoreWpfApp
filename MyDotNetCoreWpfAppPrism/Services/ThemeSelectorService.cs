using System;
using System.Windows;
using MahApps.Metro;
using Microsoft.Win32;
using MyDotNetCoreWpfAppPrism.Contracts.Services;
using MyDotNetCoreWpfAppPrism.Models;

namespace MyDotNetCoreWpfAppPrism.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private bool IsHighContrastActive
                        => SystemParameters.HighContrast;

        public ThemeSelectorService()
        {
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        public bool SetTheme(AppTheme? theme = null)
        {
            if (IsHighContrastActive)
            {
                // TODO: Set high contrast theme name
            }
            else if (theme == null)
            {
                if (App.Current.Properties.Contains("Theme"))
                {
                    // Saved theme
                    var themeName = App.Current.Properties["Theme"].ToString();
                    theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
                }
                else
                {
                    // Default theme
                    theme = AppTheme.Light;
                }
            }

            var currentTheme = ThemeManager.DetectTheme(Application.Current);
            if (currentTheme == null || currentTheme.Name != theme.ToString())
            {
                ThemeManager.ChangeTheme(Application.Current, $"{theme}.Blue");
                App.Current.Properties["Theme"] = theme.ToString();
                return true;
            }

            return false;
        }

        public AppTheme GetCurrentTheme()
        {
            var themeName = App.Current.Properties["Theme"]?.ToString();
            var theme = AppTheme.Light;
            Enum.TryParse(themeName, out theme);
            return theme;
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
