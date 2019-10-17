using MahApps.Metro.Controls;
using MyDotNetCoreWpfAppPrism.Models;
using Prism.Regions;

namespace MyDotNetCoreWpfAppPrism.Views
{
    public partial class ShellPage : MetroWindow
    {
        public ShellPage(IRegionManager regionManager, AppConfig config)
        {
            InitializeComponent();
            RegionManager.SetRegionName(hamburgerMenuContentControl, config.MainRegion);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
        }
    }
}
