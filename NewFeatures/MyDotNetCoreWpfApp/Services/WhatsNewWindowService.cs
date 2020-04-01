using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    public class WhatsNewWindowService : IWhatsNewWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemService _systemService;
        private const string CurrentVersionKey = "currentVersion";
        private bool _shown = false;

        public WhatsNewWindowService(IServiceProvider serviceProvider, ISystemService systemService)
        {
            _serviceProvider = serviceProvider;
            _systemService = systemService;
        }

        public void ShowIfAppropriate()
        {
            if (DetectIfAppUpdated() && !_shown)
            {
                _shown = true;
                var dialog = _serviceProvider.GetService(typeof(WhatsNewWindow)) as WhatsNewWindow;
                dialog.ShowDialog();
            }
        }

        private bool DetectIfAppUpdated()
        {
            var currentVersion = _systemService.GetVersion().ToString();
            if (!App.Current.Properties.Contains(CurrentVersionKey))            
            {
                App.Current.Properties[CurrentVersionKey] = currentVersion;
            }
            else
            {
                var lastVersion = App.Current.Properties[CurrentVersionKey] as string;
                if (currentVersion != lastVersion)
                {
                    App.Current.Properties[CurrentVersionKey] = currentVersion;
                    return true;
                }
            }

            return false;
        }
    }
}
