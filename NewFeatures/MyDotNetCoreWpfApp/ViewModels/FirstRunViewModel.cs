using System.Windows.Input;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class FirstRunViewModel : Observable
    {
        private FirstRunWindow _dialogWindow;
        private ICommand _closeWindowCommand;

        public ICommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(OnCloseWindow));

        public FirstRunViewModel()
        {
        }

        public void Initialize(FirstRunWindow dialogWindow)
           => _dialogWindow = dialogWindow;

        private void OnCloseWindow()
            => _dialogWindow.Close();
    }
}
