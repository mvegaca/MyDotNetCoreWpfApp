using System.Windows.Navigation;

namespace MyDotNetCoreWpfApp.Services
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object ExtraData);

        void OnNavigatingFrom();
    }
}
