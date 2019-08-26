using System;
using System.IO;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Services;

namespace MyDotNetCoreWpfApp.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private INavigationService _navigationService;
        private IFilesService _filesService;

        public event EventHandler<PersistAndRestoreArgs> OnPersistData;

        public PersistAndRestoreService(INavigationService navigationService, IFilesService filesService)
        {
            _navigationService = navigationService;
            _filesService = filesService;
        }

        public bool CanHandle(object args)
            => !_navigationService.IsNavigated();

        public async Task HandleAsync(object args)
        {
            await Task.CompletedTask;
            var persistData = GetPersistAndRestoreData();
            if (persistData != null)
            {
                _navigationService.Navigate(persistData.Target.FullName, persistData.PersistAndRestoreData);
            }
        }

        public bool PersistData()
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

                _filesService.Save(FolderPaths.Configurations, FileNames.PersistAndRestoreData, persistArgs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private PersistAndRestoreArgs GetPersistAndRestoreData()
        {
            var persistData = _filesService.Read<PersistAndRestoreArgs>(Path.Combine(FolderPaths.Configurations, FileNames.PersistAndRestoreData));
            if (persistData?.Target != null)
            {
                return persistData;
            }

            return null;
        }
    }
}
