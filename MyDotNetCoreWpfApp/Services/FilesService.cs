using MyDotNetCoreWpfApp.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Services
{
    public static class FilesService
    {
        private const string _configurationPath = @"MyDotNetCoreWpfApp\Configurations";

        public static readonly string ConfigurationFolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _configurationPath);

        public static void Initialize()
        {
            CreateFolder(ConfigurationFolderPath);
        }

        public static async Task<T> ReadAsync<T>(string filePath)
        {
            if (filePath != null && File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return await Json.ToObjectAsync<T>(json);
            }

            return default(T);
        }

        public static async Task SaveAsync<T>(string filePath, T content)
        {
            await Task.CompletedTask;
            //var fileContent = await Json.StringifyAsync(content);
            var fileContent = JsonConvert.SerializeObject(content);
            File.WriteAllText(filePath, fileContent, Encoding.UTF8);
        }

        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
