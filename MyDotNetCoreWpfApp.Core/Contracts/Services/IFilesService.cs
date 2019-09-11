namespace MyDotNetCoreWpfApp.Core.Contracts.Services
{
    public interface IFilesService
    {
        T Read<T>(string folderPath, string filePath);

        void Save<T>(string folderPath, string fileName, T content);

        void Delete(string folderPath, string filePath);
    }
}
