using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDiary.Commands;
using WpfDiary.Models;
using WpfDiary.Models.Domains;
using WpfDiary.Models.Wrappers;
using WpfDiary.Views;

namespace WpfDiary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        private DatabaseSettingsModel DbSettings = new DatabaseSettingsModel();

        public MainViewModel()
        {
            /* to ustawiamy na pierwszym uruchomieniu zeby na podstawie utworzonych klas Domains/Wrappers/Configurations utworzyl baze danych.
          using (var context = new ApplicationDbContext())
          {
              var students = context.Students.ToList();
          }
          */

            CheckConnection();


            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);

            //praca domowa 1, cz2-----------
            DatabaseSettingsCommand = new RelayCommand(DatabaseSettings);


            InitStudents();
            InitGroups();

        }

    // to jest do przycisków
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }

        //praca domowa 1, cz2-----------
        public ICommand DatabaseSettingsCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }


        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

        private void RefreshStudents(object obj)
        {
            InitStudents();
        }
        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia",
                $"Czy na pewno chcesz usunąc ucznia {SelectedStudent.FirstName}{SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            // usuwanie ucznia z bazy
            _repository.DeleteStudent(SelectedStudent.Id);

            InitStudents();
        }

        private void AddEditStudent(object obj)
        {
            //to nie jest dobra praktyka 
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);

            addEditStudentWindow.Closed += addEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
            

        }
        //praca domowa 1, cz2-----------
        private void DatabaseSettings(object obj)
        {
            //to nie jest dobra praktyka 
            var databseSettigns = new DatabaseSettingsView(obj as DbConnect);

          //  databseSettigns.Closed += addEditStudentWindow_Closed;
            databseSettigns.ShowDialog();


        }

        private void addEditStudentWindow_Closed(object sender, EventArgs e)
        {
            InitStudents();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private void InitStudents()
        {
           // var students = _repository.GetStudents();


            Students = new ObservableCollection<StudentWrapper>(
                _repository.GetStudents(SelectedGroupId));


        }

        private void InitGroups()
        {
           var groups =  _repository.GetGroups();

            groups.Insert(0, new Group{ Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);
          

            SelectedGroupId = 0;
        }

        private void CheckConnection()
        {
            if (!DbSettings.CheckConnection())
            {
                MessageBox.Show("Zostanie otwarte okono konfiguracyjne");
                var databseSettigns = new DatabaseSettingsView();
                databseSettigns.ShowDialog();

            }
        }


    }
}
