using System;

namespace MyDotNetCoreWpfPrismApp.Services
{
    public interface IPersistAndRestoreService
    {
        event EventHandler<PersistAndRestoreArgs> OnPersistData;

        bool PersistData();
    }
}
