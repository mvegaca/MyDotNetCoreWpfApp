using System;
using System.Windows;
using MahApps.Metro.Controls;
using MyDotNetCoreWpfPrismApp.ViewModels;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.Views
{
    public partial class ShellWindow : MetroWindow
    {
        private IRegionManager _regionManager;
        public ShellWindow(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var option = (HamburgerMenuGlyphItem)e.AddedItems[0];
            var viewName = (string)option.Tag;
            _regionManager.RequestNavigate("MainRegion", viewName);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ShellWindowViewModel)DataContext).Initialize();
        }
    }
}
