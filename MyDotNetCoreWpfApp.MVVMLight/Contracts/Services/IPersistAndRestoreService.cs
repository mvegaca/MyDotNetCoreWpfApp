namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.Services
{
    public interface IPersistAndRestoreService
    {
        void RestoreData();

        void PersistData();
    }
}