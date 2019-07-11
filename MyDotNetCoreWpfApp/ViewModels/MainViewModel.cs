using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable
    {
        private NavigationService _navigationService;
        private ICommand _navigateCommand;

        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new RelayCommand(OnNavigate));

        public MainViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnNavigate()
        {
            _navigationService.Navigate<SecondaryPage>("Hello world!");
        }
    }
}
