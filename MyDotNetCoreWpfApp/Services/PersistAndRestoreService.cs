using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyDotNetCoreWpfApp.Activation;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    public class PersistAndRestoreService : IActivationHandler
    {
        private string _persistAndRestoreFilePath =
            Path.Combine(FilesService.ConfigurationFolderPath, "PersistAndRestoreData.json");

        private NavigationService _navigationService;
        private ShelWindow _shelWindow;

        public event EventHandler<PersistAndRestoreArgs> OnPersistData;

        public PersistAndRestoreService(NavigationService navigationService, ShelWindow shelWindow)
        {
            _navigationService = navigationService;
            _shelWindow = shelWindow;
        }

        public bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public async Task HandleAsync(object args)
        {
            var persistData = await GetPersistAndRestoreData();
            if (persistData?.Target != null && typeof(Page).IsAssignableFrom(persistData.Target))
            {
                _shelWindow.Show();
                bool navigated = _navigationService.Navigate(persistData.Target, persistData.PersistAndRestoreData);
            }
        }

        public async Task<bool> PersistDataAsync()
        {
            if (OnPersistData == null)
            {
                return false;
            }

            try
            {
                var persistData = new PersistAndRestoreData()
                {
                    PersistDate = DateTime.Now
                };

                var target = OnPersistData.Target.GetType();
                var persistArgs = new PersistAndRestoreArgs(persistData, target);
                OnPersistData?.Invoke(this, persistArgs);

                await FilesService.SaveAsync(_persistAndRestoreFilePath, persistArgs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<PersistAndRestoreArgs> GetPersistAndRestoreData()
        {
            var persistData = await FilesService.ReadAsync<PersistAndRestoreArgs>(_persistAndRestoreFilePath);
            if (persistData?.Target != null && typeof(Page).IsAssignableFrom(persistData.Target))
            {
                return persistData;
            }

            return null;
        }
    }
}
