using MyDotNetCoreWpfApp.Helpers;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class MainViewModel : Observable
    {
        private string _data;

        public string Data
        {
            get { return _data; }
            set { Set(ref _data, value); }
        }

        public MainViewModel()
        {
        }
    }
}
