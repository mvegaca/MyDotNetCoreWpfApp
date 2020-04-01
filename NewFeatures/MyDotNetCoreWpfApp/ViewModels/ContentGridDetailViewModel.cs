using System.Linq;
using MyDotNetCoreWpfApp.Contracts.ViewModels;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Models;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class ContentGridDetailViewModel : Observable, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;
        private SampleOrder _item;

        public SampleOrder Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public ContentGridDetailViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            if (parameter is long orderID)
            {
                var data = await _sampleDataService.GetContentGridDataAsync();
                Item = data.First(i => i.OrderID == orderID);
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
