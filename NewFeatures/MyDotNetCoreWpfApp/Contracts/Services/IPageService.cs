using System;
using System.Windows.Controls;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IPageService
    {
        void ConfigureDefaultNavigation(string key, object navigationParameters = null);

        string GetDefaultNavigation();

        Type GetPageType(string key);

        Page GetPage(string key);
    }
}
