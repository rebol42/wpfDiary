using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDiary.Models;
using WpfDiary.Models.Domains;
using WpfDiary.Models.Wrappers;
using WpfDiary.Views;
using WpfDiary.Commands;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;

namespace WpfDiary.ViewModels
{
    class DatabaseSettingsModel : ViewModelBase
    {

        private ApplicationDbContext _appCbContext = new ApplicationDbContext();
       

        public DatabaseSettingsModel(DbConnect databaseSettigns = null)
        {
            SaveDbSettingsCommand = new RelayCommand(SaveDbSettigns);
            CloseDbSettingsCommand = new RelayCommand(CloseDbSettigns);
            DbConnectionTestCommand = new RelayCommand(DbTestConnection);

 
            if(databaseSettigns == null)
            {
                DatabaseSettigns = new DbConnect();
            }
            else
                DatabaseSettigns = databaseSettigns;
        }


        public ICommand SaveDbSettingsCommand { get; set; }
        public ICommand CloseDbSettingsCommand { get; set; }
        public ICommand DbConnectionTestCommand { get; set; }


        private void SaveDbSettigns(object obj)
        {
          // _databaseSettigns = obj as DbConnect;

            _appCbContext.dbConnection(_databaseSettigns);
        }

        private void CloseDbSettigns(object obj)
        {
            this.CloseWindow(obj as MetroWindow);
        }

        private void DbTestConnection(object obj)
        {
           
        }
        private void CloseWindow(MetroWindow window)
        {
            window.Close();
        }

        private DbConnect _databaseSettigns;

        public DbConnect DatabaseSettigns
        {
            get { return _databaseSettigns; }
            set
            {
                _databaseSettigns = value;
                OnPropertyChanged();
            }
        }
    }
}
