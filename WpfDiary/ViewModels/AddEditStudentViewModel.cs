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

namespace WpfDiary.ViewModels
{
    class AddEditStudentViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand    =   new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if(student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }


        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }



        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set
            {
                _student = value;
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

        private void InitGroups()
        {
            var groups = _repository.GetGroups();

            groups.Insert(0, new Group { Id = 0, Name = "-- Brak --" });

            Groups = new ObservableCollection<Group>(groups);


            SelectedGroupId = Student.Group.Id;
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }



        private void Confirm(object obj)
        {
            if (!_student.IsValid)
                return;


            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();



                // tutaj bedzie dopisana logika 
                this.CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            // aktualizacja na bazie danych
            _repository.UpdateStudent(Student);
        }

        private void AddStudent()
        {
            // dodawanie do bazy danych
            _repository.AddStudent(Student);
        }

        private void Close(object obj)
        {
            this.CloseWindow(obj as Window);
        }
    }
}
