using System;
using MyDotNetCoreWpfApp.Contracts.Services;
using Windows.ApplicationModel;

namespace MyDotNetCoreWpfApp.Services
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        public Version GetVersion()
        {
            //// MSIX distribuition
            //// Setup the App Version in MyDotNetCoreWpfApp.Packaging > Package.appxmanifest > Packaging > PackageVersion
            var version = Package.Current.Id.Version;
            return new Version(version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
