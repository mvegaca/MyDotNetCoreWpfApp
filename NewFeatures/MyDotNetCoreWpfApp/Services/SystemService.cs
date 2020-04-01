using System.Diagnostics;
using System.Reflection;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Models;
using Windows.ApplicationModel;

namespace MyDotNetCoreWpfApp.Services
{
    public class SystemService : ISystemService
    {
        public SystemService()
        {
        }

        public AppVersion GetVersion()
        {
            var version = Package.Current.Id.Version;
            return new AppVersion()
            {
                Major = version.Major,
                Minor = version.Minor,
                Build = version.Build,                
                Revision = version.Revision
            };

            //string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            //var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion.Split('.');
            //return new AppVersion()
            //{
            //    Major = ushort.Parse(version[0]),
            //    Minor = ushort.Parse(version[1]),
            //    Build = ushort.Parse(version[2]),            
            //    Revision = ushort.Parse(version[3])
            //};
        }

        public void OpenInWebBrowser(string url)
        {
            // For more info see https://github.com/dotnet/corefx/issues/10361
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
