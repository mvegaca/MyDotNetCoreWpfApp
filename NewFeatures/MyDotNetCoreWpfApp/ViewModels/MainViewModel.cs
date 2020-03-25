using System;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly IToastNotificationService _toastNotificationService;

        public MainViewModel(IToastNotificationService toastNotificationService)
        {
            _toastNotificationService = toastNotificationService;
            _toastNotificationService.ShowToast("Hello from MainViewModel");
        }
    }
}
