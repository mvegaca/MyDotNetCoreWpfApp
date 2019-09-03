using System;
using System.Windows.Threading;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfPrismApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private IRegionManager _regionManager;
        private IThemeSelectorService _themeSelectorService;
        private int _data;
        private DispatcherTimer _timer;

        public int Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public DelegateCommand SetLightThemeCommand { get; private set; }

        public DelegateCommand SetDarkThemeCommand { get; private set; }

        public MainViewModel(IRegionManager regionManager, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService)
        {
            NavigateCommand = new DelegateCommand(OnNavigate);
            SetLightThemeCommand = new DelegateCommand(OnSetLightTheme);
            SetDarkThemeCommand = new DelegateCommand(OnSetDarkTheme);
            _regionManager = regionManager;
            _themeSelectorService = themeSelectorService;
            persistAndRestoreService.OnPersistData += OnPersistData;
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
        }

        private void OnPersistData(object sender, PersistAndRestoreArgs e)
        {
            e.PersistAndRestoreData.Data = Data;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Data = Data + 1;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _timer.Stop();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //if (navigationContext.Parameters[0] is PersistAndRestoreData restoreData)
            //{
            //    Data = int.Parse(restoreData.Data.ToString());
            //}

            _timer.Start();
        }

        private void OnNavigate()
        {
            var parameters = new NavigationParameters();
            parameters.Add("pram1", "Navigation data as navigation parameter!");
            _regionManager.RequestNavigate(RegionNames.MainRegion, "Secondary", parameters);
        }

        private void OnSetLightTheme()        
            => _themeSelectorService.SetTheme(Themes.BaseLightTheme);

        private void OnSetDarkTheme()
            => _themeSelectorService.SetTheme(Themes.BaseDarkTheme);
    }
}
