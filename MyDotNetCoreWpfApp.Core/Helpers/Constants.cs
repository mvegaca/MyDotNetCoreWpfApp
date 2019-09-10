using System;
using System.IO;

namespace MyDotNetCoreWpfApp.Core.Helpers
{
    public class Constants
    {
        public const string ThemeLight = "Light.Blue";
        public const string ThemeDark = "Dark.Blue";
        private const string _configurationsPath = @"MyDotNetCoreWpfApp\Configurations";

        public static readonly string FolderConfigurations =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _configurationsPath);
        
        public const string FileNameAppProperties = "AppProperties.txt";

        public const string FileNamePersistAndRestoreData = "PersistAndRestoreData.json";

        public const string RegionMain = "RegionMain";
    }
}
