using System.Collections.Generic;

namespace MyDotNetCoreWpfApp.Core.Services
{
    public interface IIsolatedStorageService
    {
        IEnumerable<string> ReadLines(string fileName);

        T Read<T>(string fileName);

        void SaveLines(string fileName, IEnumerable<string> lines);

        void Save<T>(string fileName, T content);
    }
}
