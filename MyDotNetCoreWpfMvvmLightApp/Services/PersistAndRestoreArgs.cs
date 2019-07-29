namespace MyDotNetCoreWpfMvvmLightApp.Services
{
    public class PersistAndRestoreArgs
    {
        public PersistAndRestoreData PersistAndRestoreData { get; set; }

        public string ViewModelName { get; private set; }

        public PersistAndRestoreArgs(PersistAndRestoreData persistAndRestoreData, string viewModelName)
        {
            PersistAndRestoreData = persistAndRestoreData;
            ViewModelName = viewModelName;
        }
    }
}
