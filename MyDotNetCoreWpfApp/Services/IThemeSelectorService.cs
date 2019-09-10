namespace MyDotNetCoreWpfApp.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(string themeName = null);

        string GetCurrentThemeName();
    }
}
