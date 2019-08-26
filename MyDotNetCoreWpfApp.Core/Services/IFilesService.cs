namespace MyDotNetCoreWpfApp.Core.Services
{
    public interface IFilesService
    {
        T Read<T>(string filePath);

        void Save<T>(string folderPath, string fileName, T content);
    }
}
