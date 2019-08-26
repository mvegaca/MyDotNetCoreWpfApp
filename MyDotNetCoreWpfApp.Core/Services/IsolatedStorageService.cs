using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace MyDotNetCoreWpfApp.Core.Services
{
    public class IsolatedStorageService : IIsolatedStorageService
    {
        public IEnumerable<string> ReadLines(string fileName)
        {
            var storage = IsolatedStorageFile.GetUserStoreForDomain();
            if (storage.FileExists(fileName))
            {
                using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }

        public T Read<T>(string fileName)
        {
            var storage = IsolatedStorageFile.GetUserStoreForDomain();
            if (storage.FileExists(fileName))
            {
                using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var data = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(data);
                    }
                }
            }

            return default(T);
        }

        public void SaveLines(string fileName, IEnumerable<string> lines)
        {
            var storage = IsolatedStorageFile.GetUserStoreForDomain();

            using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            using (var writer = new StreamWriter(stream))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public void Save<T>(string fileName, T content)
        {
            var storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            using (var writer = new StreamWriter(stream))
            {
                var fileContent = JsonConvert.SerializeObject(content);
                writer.Write(fileContent);
            }
        }
    }
}
