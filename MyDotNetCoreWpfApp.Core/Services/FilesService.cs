using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MyDotNetCoreWpfApp.Core.Services
{
    public class FilesService : IFilesService
    {
        public T Read<T>(string filePath)
        {
            if (filePath != null && File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }

        public void Save<T>(string folderPath, string fileName, T content)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileContent = JsonConvert.SerializeObject(content);
            File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
        }
    }
}
