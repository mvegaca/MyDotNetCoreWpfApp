using MyDotNetCoreWpfApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SecondaryViewModel : Observable
    {
        private string _navigationExtraData;

        public string NavigationExtraData
        {
            get { return _navigationExtraData; }
            set { Set(ref _navigationExtraData, value); }
        }

        public SecondaryViewModel()
        {
        }

        public void LoadData(string extraData)
        {
            NavigationExtraData = extraData;
        }
    }
}
