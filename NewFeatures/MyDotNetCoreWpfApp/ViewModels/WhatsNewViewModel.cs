using System.Windows.Input;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class WhatsNewViewModel : Observable
    {
        private WhatsNewWindow _dialogWindow;
        private ICommand _closeWindowCommand;

        public ICommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(OnCloseWindow));

        public WhatsNewViewModel()
        {
        }

        public void Initialize(WhatsNewWindow dialogWindow)
           => _dialogWindow = dialogWindow;

        private void OnCloseWindow()
            => _dialogWindow.Close();
    }
}
