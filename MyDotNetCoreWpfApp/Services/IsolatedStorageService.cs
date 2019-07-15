using MyDotNetCoreWpfApp.Core.Helpers;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace MyDotNetCoreWpfApp.Services
{
    public enum AppDirectory
    {
        LocalAppData
    }

    public static class IsolatedStorageService
    {
        public static IEnumerable<string> ReadLines(string fileName)
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

        public static async Task<T> ReadAsync<T>(string fileName)
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
                        return await Json.ToObjectAsync<T>(data);
                    }
                }
            }

            return default(T);
        }

        public static void SaveLines(string fileName, IEnumerable<string> lines)
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

        public static async Task SaveAsync<T>(string fileName, T content)
        {
            var storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            using (var writer = new StreamWriter(stream))
            {
                var fileContent = await Json.StringifyAsync(content);
                writer.Write(fileContent);
            }
        }
    }
}
