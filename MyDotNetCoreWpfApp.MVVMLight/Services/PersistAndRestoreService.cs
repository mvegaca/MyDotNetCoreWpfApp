using System;
using System.Collections;
using System.IO;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Contracts.Services;
using MyDotNetCoreWpfApp.MVVMLight.Models;

namespace MyDotNetCoreWpfApp.MVVMLight.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFilesService _filesService;

        private string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public PersistAndRestoreService(IFilesService filesService)
        {
            _filesService = filesService;
        }

        public void PersistData()
        {
            if (App.Current.Properties != null)
            {
                var folderPath = Path.Combine(_localAppData, AppConfig.ConfigurationsFolder);
                var fileName = AppConfig.AppPropertiesFileName;
                _filesService.Save(folderPath, fileName, App.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var folderPath = Path.Combine(_localAppData, AppConfig.ConfigurationsFolder);
            var fileName = AppConfig.AppPropertiesFileName;
            var properties = _filesService.Read<IDictionary>(folderPath, fileName);
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
