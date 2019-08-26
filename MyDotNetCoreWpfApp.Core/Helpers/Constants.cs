using System;
using System.IO;

namespace MyDotNetCoreWpfApp.Core.Helpers
{
    public class Themes
    {
        public const string BaseLightTheme = "Light.Blue";
        public const string BaseDarkTheme = "Dark.Blue";
    }

    public class FolderPaths
    {
        private const string _configurationsPath = @"MyDotNetCoreWpfApp\Configurations";

        public static readonly string Configurations =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _configurationsPath);
    }

    public class FileNames
    {
        public const string AppProperties = "AppProperties.txt";

        public const string PersistAndRestoreData = "PersistAndRestoreData.json";
    }
}
