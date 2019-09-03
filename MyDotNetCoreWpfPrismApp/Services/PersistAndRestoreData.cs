using System;
using Prism.Regions;

namespace MyDotNetCoreWpfPrismApp.Services
{
    public class PersistAndRestoreData
    {
        public object Data { get; set; }

        public DateTime PersistDate { get; set; }


        public NavigationParameters GetNavigationParameters()
        {
            var parameters = new NavigationParameters();
            parameters.Add(nameof(PersistAndRestoreData), Data);
            return parameters;
        }
    }
}
