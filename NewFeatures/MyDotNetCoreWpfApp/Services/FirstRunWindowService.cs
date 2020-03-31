using System;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    public class FirstRunWindowService : IFirstRunWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private const string IsFirstRunKey = "IsFirstRun";
        private bool _shown = false;

        public FirstRunWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowIfAppropriate()
        {
            if (DetectIfFirstUse() && !_shown)
            {
                _shown = true;
                var dialog = _serviceProvider.GetService(typeof(FirstRunWindow)) as FirstRunWindow;
                dialog.ShowDialog();
            }
        }

        private bool DetectIfFirstUse()
        {
            if (App.Current.Properties.Contains(IsFirstRunKey))
            {
                return false;
            }

            App.Current.Properties[IsFirstRunKey] = true;
            return true;
        }
    }
}
