using System;
using System.IO;

namespace MyDotNetCoreWpfApp.Configuration
{
    public class AppConfig
    {
        private readonly string _storageFolder
            = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public string ConfigurationsPath { get; set; }

        public string DataPath { get; set; }

        public string FileNameAppProperties { get; set; }

        public string FolderConfigurations =>
            Path.Combine(_storageFolder, ConfigurationsPath);
    }
}