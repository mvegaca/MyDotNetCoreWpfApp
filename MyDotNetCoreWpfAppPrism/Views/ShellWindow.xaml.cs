using MahApps.Metro.Controls;
using MyDotNetCoreWpfAppPrism.Models;
using Prism.Regions;

namespace MyDotNetCoreWpfAppPrism.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(hamburgerMenuContentControl, AppConfig.MainRegion);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
        }
    }
}
