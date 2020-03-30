using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            //// TODO WTS: Uncomment the following code to test the Secured Web API
            /// You must also set multiple startup projects (App + WebAPI)
            //var token = await _identityService.GetAccessTokenForWebApiAsync();
            //var data = await _httpDataService.GetAsync<IEnumerable<SampleOrder>>("api/orders", token);
            await Task.CompletedTask;
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
