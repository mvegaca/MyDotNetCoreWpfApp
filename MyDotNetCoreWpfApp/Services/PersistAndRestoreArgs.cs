using System;

namespace MyDotNetCoreWpfApp.Services
{
    public class PersistAndRestoreArgs
    {
        public PersistAndRestoreData PersistAndRestoreData { get; set; }

        public Type Target { get; private set; }

        public PersistAndRestoreArgs(PersistAndRestoreData persistAndRestoreData, Type target)
        {
            PersistAndRestoreData = persistAndRestoreData;
            Target = target;
        }
    }
}
