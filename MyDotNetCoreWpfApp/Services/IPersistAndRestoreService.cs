using System;
using MyDotNetCoreWpfApp.Activation;

namespace MyDotNetCoreWpfApp.Services
{
    public interface IPersistAndRestoreService : IActivationHandler
    {
        event EventHandler<PersistAndRestoreArgs> OnPersistData;

        bool PersistData();
    }
}
