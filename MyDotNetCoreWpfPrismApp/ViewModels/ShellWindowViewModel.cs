using Prism.Mvvm;

namespace MyDotNetCoreWpfPrismApp.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private string _title = "MyDotNetCoreWpfPrismApp";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShellWindowViewModel()
        {
        }
    }
}
