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
using WpfDiary.Properties;

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
                DatabaseSettigns = new DbConnect
                {
                    Server = Settings.Default.Server,
                    ServerDbName = Settings.Default.ServerDbName,
                    Database = Settings.Default.Database,
                    User = Settings.Default.User,
                    Password = Settings.Default.Password
            };
            }
            else
                DatabaseSettigns = databaseSettigns;
        }


        public ICommand SaveDbSettingsCommand { get; set; }
        public ICommand CloseDbSettingsCommand { get; set; }
        public ICommand DbConnectionTestCommand { get; set; }


        private void SaveDbSettigns(object obj)
        {
            _appCbContext.dbConnection(_databaseSettigns);
        }

        private void CloseDbSettigns(object obj)
        {
            this.CloseWindow(obj as MetroWindow);
        }

        private void DbTestConnection(object obj)
        {
            _appCbContext.changeConnectionString(_databaseSettigns);
   

            if (this.CheckConnection())
            {
                MessageBox.Show("Prawidłowe połączenie z bazą danych");
            }

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


        public bool CheckConnection()
        {
            bool flag = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                    flag = true;
                    
                }

            }
            catch (Exception t)
            {
                MessageBox.Show("Błąd połączenia z baza danych");
                flag = false;
            }
            return flag;
        }
    }
}
