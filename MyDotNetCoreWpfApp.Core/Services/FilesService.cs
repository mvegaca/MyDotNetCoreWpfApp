using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MyDotNetCoreWpfApp.Core.Services
{
    public class FilesService : IFilesService
    {
        public T Read<T>(string folderPath, string filePath)
        {
            var path = Path.Combine(folderPath, filePath);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
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

        public void Delete(string folderPath, string filePath)
        {
            if (filePath != null && File.Exists(Path.Combine(folderPath, filePath)))
            {
                File.Delete(Path.Combine(folderPath, filePath));
            }
        }
    }
}
