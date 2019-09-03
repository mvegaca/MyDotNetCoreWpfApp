using System;
using System.IO;
using System.Threading.Tasks;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Services;

namespace MyDotNetCoreWpfPrismApp.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private IFilesService _filesService;

        public event EventHandler<PersistAndRestoreArgs> OnPersistData;

        public PersistAndRestoreService(IFilesService filesService)
        {
            _filesService = filesService;
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

        public PersistAndRestoreArgs GetPersistAndRestoreData()
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
