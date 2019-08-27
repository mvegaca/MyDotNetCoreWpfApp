using System;
using MyDotNetCoreWpfMvvmLightApp.Activation;

namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public interface IPersistAndRestoreService : IActivationHandler
    {
        event EventHandler<PersistAndRestoreArgs> OnPersistData;

        bool PersistData();
    }
}
