using System;
using System.Collections.Generic;
using System.Linq;

namespace MyDotNetCoreWpfApp.Models
{
    public static class SchemeActivationConfig
    {
        private static readonly Dictionary<string, string> _activationPages = new Dictionary<string, string>()
        {
            // TODO WTS: Add the pages that can be opened from scheme activation in your app here.
            { "sample", typeof(ViewModels.SchemeActivationSampleViewModel).FullName }
        };

        public static string GetViewModelName(string pageKey)
        {
            return _activationPages
                    .FirstOrDefault(p => p.Key == pageKey)
                    .Value;
        }

        public static string GetPageKey(string viewModelName)
        {
            return _activationPages
                    .FirstOrDefault(v => v.Value == viewModelName)
                    .Key;
        }
    }
}
