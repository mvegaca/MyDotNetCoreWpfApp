using System;
using System.Collections.Generic;
using MyDotNetCoreWpfApp.Contracts.ViewModels;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Models;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable, INavigationAware
    {
        private readonly IIdentityService _identityService;
        private readonly IHttpDataService _httpDataService;

        public MainViewModel(IIdentityService identityService, IHttpDataService httpDataService)
        {
            _identityService = identityService;
            _httpDataService = httpDataService;
            _httpDataService.Initialize("mainViewModel", "http://localhost:53848");
        }

        public async void OnNavigatedTo(object parameter)
        {
            var token = await _identityService.GetAccessTokenForWebApiAsync();
            var data = await _httpDataService.GetAsync<IEnumerable<SampleOrder>>("api/orders", token);
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
