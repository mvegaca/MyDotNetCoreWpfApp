namespace MyDotNetCoreWpfApp.MVVMLight.Contracts.ViewModels
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object ExtraData);

        void OnNavigatingFrom();
    }
}
