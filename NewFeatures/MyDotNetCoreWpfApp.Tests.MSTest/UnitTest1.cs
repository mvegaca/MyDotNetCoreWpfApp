using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Contracts.Views;
using MyDotNetCoreWpfApp.Core.Contracts.Services;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Models;
using MyDotNetCoreWpfApp.Services;
using MyDotNetCoreWpfApp.Tests.MSTest.Services;
using MyDotNetCoreWpfApp.ViewModels;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Tests.MSTest
{
    // Read more about unit testing best practices with .NET Core and .NET Standard
    // https://docs.microsoft.com/dotnet/core/testing/
    // https://docs.microsoft.com/dotnet/core/testing/unit-testing-best-practices    
    [TestClass]
    public class UnitTest1
    {
        private IHost _host;

        public UnitTest1()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                    .ConfigureServices(ConfigureServices)
                    .Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IIdentityCacheService, IdentityCacheService>();
            services.AddHttpClient("msgraph", client =>
            {
                client.BaseAddress = new System.Uri("https://graph.microsoft.com/v1.0/");
            });

            // Core Services
            services.AddTransient<IHttpDataService, HttpDataService>();
            services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IFileService, FileService>();

            // Services
            services.AddSingleton<IBackgroundTaskService, BackgroundTaskService>();
            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<ISystemService, SystemService>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
            services.AddSingleton<IThemeSelectorService, MockThemeSelectorService>();

            // Views and ViewModels
            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<SettingsViewModel>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestFileService()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folderPath = Path.Combine(localAppData, "UnitTests");
            var fileService = _host.Services.GetService(typeof(IFileService)) as IFileService;
            fileService.Save(folderPath, "UnitTest1.json", "Lorem ipsum dolor sit amet");
            var cacheData = fileService.Read<string>(folderPath, "UnitTest1.json");
            Assert.IsTrue(cacheData.Equals("Lorem ipsum dolor sit amet"));
        }

        // TODO WTS: Add tests for functionality you add to MainViewModel.
        [TestMethod]
        public void TestMainViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(MainViewModel));
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to MainViewModel.
        [TestMethod]
        public void TestSettingsViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(SettingsViewModel)) as SettingsViewModel;            
            Assert.IsNotNull(vm);
        }
    }
}
