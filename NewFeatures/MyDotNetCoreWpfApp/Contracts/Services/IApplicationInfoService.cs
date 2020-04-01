using System;

namespace MyDotNetCoreWpfApp.Contracts.Services
{
    public interface IApplicationInfoService
    {
        Version GetVersion();
    }
}
