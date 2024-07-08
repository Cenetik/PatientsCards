using System;
using System.Configuration;
using System.Data;
using System.Windows;
using App.Exceptions;
using App.Services;
using DataAccess.RepositoryImpls;
using Domain.Models;
using Domain;
using PatientsCards;
using PatientsCardsUI.Views;
using PatientsCardsUI.ViewModels;

namespace PatientsCardsUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private InMemoryBaseRepository<Doctor> doctorsRepository;
        private InMemoryBaseRepository<Patient> patientsRepository;
        private InMemoryBaseRepository<Visit> visitsRepository;
        private InMemoryBaseRepository<User> usersRepository;

        public App()
        {
            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
            //_serviceIdentifyProvider = serviceCollection.BuildServiceProvider();
        }

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    // Регистрация сервисов
        //    //services.AddTransient<MainWindow>();
        //    // Здесь можно добавить другие сервисы, например:
        //    // services.AddTransient<IYourService, YourServiceImplementation>();
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            FillFakeData();

            var loginWindow = new LoginWindow(new UsersService(usersRepository));
            loginWindow.ShowDialog();
            

           // var mainWindow = new MainWindow();
          //  mainWindow.DataContext = mainModel;
            
            //mainWindow.Show();
        }

        private void FillFakeData()
        {
            var fakeDataFactory = new FakeDataFactory();
            doctorsRepository = new InMemoryBaseRepository<Doctor>(fakeDataFactory.Doctors);
            patientsRepository = new InMemoryBaseRepository<Patient>(fakeDataFactory.Patients);
            visitsRepository = new InMemoryBaseRepository<Visit>(fakeDataFactory.Visits);
            usersRepository = new InMemoryBaseRepository<User>(fakeDataFactory.Users);
        }       

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is ValidateException)
            {
                MessageBox.Show("Ошибка проверки полей: " + e.Exception.Message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                e.Handled = true;
                return;
            }

            MessageBox.Show("Неожиданная ошибка: " + e.Exception.Message, "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;

        }
    }
}
