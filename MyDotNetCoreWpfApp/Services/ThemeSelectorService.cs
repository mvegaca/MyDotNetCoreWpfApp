using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyDotNetCoreWpfApp.Services
{
    public class ThemeSelectorService
    {
        public const string LightTheme = "Light";
        public const string DarkTheme = "Dark";
        public const string HighContrastTheme = "HighContrast";

        private ResourceDictionary themeResourceDictionaty;
        private bool _isHighContrastActive
                        => SystemParameters.HighContrast;

        private readonly Dictionary<string, Uri> _brushes =
            new Dictionary<string, Uri>()
            {
                { LightTheme, new Uri("/Styles/LightBrushes.xaml", UriKind.Relative) },
                { DarkTheme, new Uri("/Styles/DarkBrushes.xaml", UriKind.Relative) },
                { HighContrastTheme, new Uri("/Styles/HighContrastBrushes.xaml", UriKind.Relative) }
            };

        public ThemeSelectorService()
        {
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        public void SetTheme(string theme = null)
        {
            try
            {
                if (_isHighContrastActive)
                {
                    theme = HighContrastTheme;
                }
                else if (string.IsNullOrEmpty(theme))
                {
                    if (App.Current.Properties.Contains("Theme"))
                    {
                        // Saved theme
                        theme = App.Current.Properties["Theme"]?.ToString();
                    }
                    else
                    {
                        // Default theme
                        theme = LightTheme;
                    }
                }

                App.Current.Resources.MergedDictionaries.Remove(this.themeResourceDictionaty);
                themeResourceDictionaty = new ResourceDictionary() { Source = _brushes[theme] };
                App.Current.Resources.MergedDictionaries.Add(this.themeResourceDictionaty);
                App.Current.Properties["Theme"] = theme.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Color || e.Category == UserPreferenceCategory.VisualStyle)
            {
                SetTheme();
            }
        }
    }
}