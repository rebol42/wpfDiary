﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WpfDiary.Models;
using WpfDiary.ViewModels;

namespace WpfDiary.Views
{
    /// <summary>
    /// Interaction logic for DatabaseSettingsView.xaml
    /// </summary>
    public partial class DatabaseSettingsView : MetroWindow
    {
        //private DatabaseSettingsModel DatabaseConnect;

        public DatabaseSettingsView(DbConnect dbConnect = null)
        {
            InitializeComponent();
            DataContext = new DatabaseSettingsModel(dbConnect);
        }
    }
}
