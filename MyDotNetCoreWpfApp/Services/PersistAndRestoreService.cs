using System.Collections;
using System.Collections.Generic;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private IFilesService _filesService;

        public PersistAndRestoreService(IFilesService filesService)
        {
            _filesService = filesService;
        }

        public void PersistData()
        {
            if (App.Current.Properties != null)
            {
                _filesService.Save(Constants.FolderConfigurations, Constants.FileNameAppProperties, App.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var properties = _filesService.Read<IDictionary>(Constants.FolderConfigurations, Constants.FileNameAppProperties);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    App.Current.Properties.Add(property.Key, property.Value);
                }
            }
        }
    }
}
