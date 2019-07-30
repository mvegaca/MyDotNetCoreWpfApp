using System;
using System.Globalization;
using System.Windows;
using MyDotNetCoreWpfPrismApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
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

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ModuleA.ModuleAModule>();
        }
    }
}
