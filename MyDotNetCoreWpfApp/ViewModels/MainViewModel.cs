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
        private ICommand _navigateCommand;

        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new RelayCommand(OnNavigate));

        public MainViewModel()
        {
        }

        private void OnNavigate()
        {
            App.CurrentApp.NavigationService.Navigate<SecondaryPage>("Hello world!");
        }
    }
}
