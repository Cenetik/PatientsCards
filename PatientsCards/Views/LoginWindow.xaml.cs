using App.Exceptions;
using App.Services;
using PatientsCards;
using System;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UsersService usersService;

        public LoginWindow(UsersService usersService)
        {
            InitializeComponent();
            this.usersService = usersService;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {            
            var errorMessage = usersService.Login(textBoxName.Text, textBoxPassword.Password);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errormessage.Text = errorMessage;
                return;
            }
            var mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
            //Application.Current.MainWindow.Close();
            //this.DialogResult = true;
        }
    }
}
