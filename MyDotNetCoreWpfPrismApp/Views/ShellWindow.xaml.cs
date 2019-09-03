using MahApps.Metro.Controls;
using MyDotNetCoreWpfApp.Core.Helpers;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.Views
{
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(hamburgerMenuContentControl, RegionNames.MainRegion);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
        }
    }
}
