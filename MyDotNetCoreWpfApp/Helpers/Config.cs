using System;
using System.IO;

namespace MyDotNetCoreWpfApp.Helpers
{
    public class Config
    {
        private const string _configurationsPath = @"MyDotNetCoreWpfApp\Configurations";
        private const string _dataPath = @"MyDotNetCoreWpfApp\Data";

        private static readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Files and folders
        public static readonly string FolderConfigurations =
            Path.Combine(_localApplicationData, _configurationsPath);

        public static readonly string FolderData =
            Path.Combine(_localApplicationData, _dataPath);

        public const string FileNameAppProperties = "AppProperties.txt";
    }
}
