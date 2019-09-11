namespace MyDotNetCoreWpfApp.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(string themeName = null);

        bool IsLightThemeSelected();

        bool IsDarkThemeSelected();

        string GetCurrentThemeName();
    }
}
