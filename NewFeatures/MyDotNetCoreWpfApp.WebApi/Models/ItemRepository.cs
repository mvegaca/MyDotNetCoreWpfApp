using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Models;
using MyDotNetCoreWpfApp.Core.Services;

namespace MyDotNetCoreWpfApp.WebApi.Models
{
    // TODO WTS: Replace or update this class when no longer using sample data.
    public class ItemRepository : IItemRepository
    {
        private readonly ISampleDataService _sampleDataService = new SampleDataService();
        private static Dictionary<string, SampleCompany> items =
            new Dictionary<string, SampleCompany>();

        public ItemRepository()
        {
            Task.Run(async () =>
            {
                foreach (var company in await _sampleDataService.GetWebApiSampleDataAsync())
                {
                    items.TryAdd(company.CompanyID, company);
                }
            });
        }

        public IEnumerable<SampleCompany> GetAll()
        {
            return items.Values;
        }

        public void Add(SampleCompany item)
        {
            items[item.CompanyID] = item;
        }

        public SampleCompany Get(string id)
        {
            items.TryGetValue(id, out SampleCompany item);

            return item;
        }

        public SampleCompany Remove(string id)
        {
            items.Remove(id, out SampleCompany item);

            return item;
        }

        public void Update(SampleCompany item)
        {
            items[item.CompanyID] = item;
        }
    }
}
