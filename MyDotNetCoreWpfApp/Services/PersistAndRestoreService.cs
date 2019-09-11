﻿using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyDotNetCoreWpfApp.Configuration;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private IFilesService _filesService;
        private AppConfig _config;

        public PersistAndRestoreService(IFilesService filesService, IOptions<AppConfig> _options)
        {
            _filesService = filesService;
            _config = _options.Value;
        }

        public void PersistData()
        {
            if (App.Current.Properties != null)
            {
                var folderName = _config.FolderConfigurations;
                var fileName = _config.FileNameAppProperties;
                _filesService.Save(folderName, fileName, App.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var folderName = _config.FolderConfigurations;
            var fileName = _config.FileNameAppProperties;
            var properties = _filesService.Read<IDictionary>(folderName, fileName);
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
