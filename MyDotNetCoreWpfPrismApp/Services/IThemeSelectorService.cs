using System;
using System.Collections.Generic;
using System.Text;

namespace MyDotNetCoreWpfPrismApp.Services
{
    public interface IThemeSelectorService
    {
        void SetTheme(string themeName = null);
    }
}
