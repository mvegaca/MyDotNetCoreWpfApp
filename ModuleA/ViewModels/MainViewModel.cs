using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleA.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        public DelegateCommand NavigateCommand { get; private set; }

        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand(OnNavigate);
        }

        private void OnNavigate()
        {
            _regionManager.RequestNavigate("ContentRegion", "Secondary");
        }
    }
}
