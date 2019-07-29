using System;
using System.IO;
using System.Threading.Tasks;
using MyDotNetCoreWpfMvvmLightApp.Activation;
using MyDotNetCoreWpfMvvmLightApp.Views;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public class PersistAndRestoreService : IActivationHandler
    {
        private string _persistAndRestoreFilePath =
            Path.Combine(FilesService.ConfigurationFolderPath, "PersistAndRestoreData.json");

        private NavigationService _navigationService;
        private ShellWindow _shelWindow;

        public event EventHandler<PersistAndRestoreArgs> OnPersistData;

        public PersistAndRestoreService(NavigationService navigationService, ShellWindow shelWindow)
        {
            _navigationService = navigationService;
            _shelWindow = shelWindow;
        }

        public bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public async Task HandleAsync(object args)
        {
            var persistData = await GetPersistAndRestoreData();
            if (!string.IsNullOrEmpty(persistData?.ViewModelName))
            {
                _shelWindow.Show();
                bool navigated = _navigationService.Navigate(persistData.ViewModelName, persistData.PersistAndRestoreData);
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
                var persistArgs = new PersistAndRestoreArgs(persistData, target.FullName);
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
            if (!string.IsNullOrEmpty(persistData?.ViewModelName))
            {
                return persistData;
            }

            return null;
        }
    }
}
