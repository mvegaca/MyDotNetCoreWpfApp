using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private HamburgerMenuItem _selectedMenuItem;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Main", Glyph = "\uE8A5", Tag = "Main" },
            new HamburgerMenuGlyphItem() { Label = "Secondary", Glyph = "\uE8A5", Tag = "Secondary" }
        };

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public DelegateCommand SelectionChangedCommand { get; private set; }

        public DelegateCommand LoadedCommand { get; private set; }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            LoadedCommand = new DelegateCommand(OnLoaded);
        }

        private void OnLoaded()
        {
            SelectedMenuItem = MenuItems.First();
            OnSelectionChanged();
        }

        private void Navigate(string navigationPath)
            => _regionManager.RequestNavigate("MainRegion", navigationPath);

        private void OnSelectionChanged()
        {
            var viewName = (string)SelectedMenuItem.Tag;
            _regionManager.RequestNavigate("MainRegion", viewName);
        }
    }
}