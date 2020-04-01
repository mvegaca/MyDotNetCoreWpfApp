using System.Collections.Generic;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Core.Models;

namespace MyDotNetCoreWpfApp.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleCompany>> GetWebApiSampleDataAsync();

        Task<IEnumerable<SampleOrder>> GetDataGridAsync();

        Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();
    }
}
