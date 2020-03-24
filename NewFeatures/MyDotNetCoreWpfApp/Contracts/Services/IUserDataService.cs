using System;

using MyDotNetCoreWpfApp.ViewModels;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IUserDataService
    {
        event EventHandler<UserViewModel> UserDataUpdated;

        void Initialize();

        UserViewModel GetUser();
    }
}
