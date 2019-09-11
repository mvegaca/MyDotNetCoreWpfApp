using System.Windows.Navigation;
using MyDotNetCoreWpfApp.Core.Helpers;
using MyDotNetCoreWpfApp.Core.Models;
using MyDotNetCoreWpfApp.Core.Services;
using MyDotNetCoreWpfApp.Helpers;
using MyDotNetCoreWpfApp.Services;

namespace MyDotNetCoreWpfApp.ViewModels
{
    public class SecondaryViewModel : Observable, INavigationAware
    {
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

        public SecondaryViewModel(IFilesService filesService)
        {
            _filesService = filesService;
        }

        public void OnNavigatedTo(object extraData)
        {
            var student = _filesService.Read<Student>(Config.FolderData, "Student.json");
            if (student != null)
            {
                Name = student.Name;
                Surname = student.Surname;
                Age = student.Age;
            }
        }

        public void OnNavigatingFrom()
        {
        }

        private void SaveData()
        {
            var student = new Student()
            {
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            _filesService.Save(Config.FolderData, "Student.json", student);
        }
    }
}
