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

namespace PatientsCardsUI.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditVisitWindow.xaml
    /// </summary>
    public partial class AddEditVisitWindow : Window
    {
        public AddEditVisitWindow()
        {
            InitializeComponent();
        }

        

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
