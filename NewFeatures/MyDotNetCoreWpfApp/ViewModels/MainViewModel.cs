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
        private readonly IHttpDataService _httpDataService;

        public MainViewModel(IHttpDataService httpDataService)
        {
            _httpDataService = httpDataService;
            _httpDataService.Initialize("mainViewModel", "http://localhost:53848");
        }

        public void OnNavigatedTo(object parameter)
        {
            //var data = await _httpDataService.GetAsync<IEnumerable<SampleCompany>>("Item/List");
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
