using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyDotNetCoreWpfPrismApp.Views;
using Prism.Ioc;
using Prism.Unity;

namespace MyDotNetCoreWpfPrismApp
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => Container.Resolve<ShellWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
