using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MyDotNetCoreWpfApp.Contracts.ViewModels;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SchemeActivationSampleViewModel : Observable, INavigationAware
    {
        public ObservableCollection<string> Parameters { get; } = new ObservableCollection<string>();

        public SchemeActivationSampleViewModel()
        {
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Dictionary<string, string> parameters)
            {
                Parameters.Clear();
                foreach (var param in parameters)
                {
                    if (param.Key == "ticks" && long.TryParse(param.Value, out long ticks))
                    {
                        var dateTime = new DateTime(ticks);
                        Parameters.Add($"{param.Key}: {dateTime.ToString()}");
                    }
                    else
                    {
                        Parameters.Add($"{param.Key}: {param.Value}");
                    }
                }
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
