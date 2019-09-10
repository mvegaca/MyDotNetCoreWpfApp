using System;
using System.Windows.Input;
using System.Windows.Navigation;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Models;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;
using Newtonsoft.Json;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SecondaryViewModel : Observable
    {
        private INavigationService _navigationService;
        private IFilesService _filesService;
        private string _name;
        private string _surname;
        private int _age;

        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                SaveData();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                Set(ref _surname, value);
                SaveData();
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                Set(ref _age, value);
                SaveData();
            }
        }

        public SecondaryViewModel(INavigationService navigationService, IFilesService filesService)
        {
            _navigationService = navigationService;
            _filesService = filesService;
            _navigationService.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.IsFromViewModel())
            {
                LoadData();
                _navigationService.Navigated -= OnNavigated;
            }
        }

        private void LoadData()
        {
            var student = _filesService.Read<Student>(Constants.FolderConfigurations, "Student.json");
            if (student != null)
            {
                Name = student.Name;
                Surname = student.Surname;
                Age = student.Age;
            }
        }

        private void SaveData()
        {
            var student = new Student()
            {
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            _filesService.Save(Constants.FolderConfigurations, "Student.json", student);
        }
    }
}
