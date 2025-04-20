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
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;

namespace PatientsCardsUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IRepository<Doctor> doctorsRepository;
        private IRepository<Patient> patientsRepository;
        private IRepository<Visit> visitsRepository;
        private IRepository<User> usersRepository;

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



            //FillFakeDataInMemory();
           // FillEfFakeData();
            GlobalData.Context = GetNewContext();

            var userRepository = new EfBaseRepository<User>(GlobalData.Context);

            var loginWindow = new LoginWindow(new UsersService(userRepository));
            loginWindow.ShowDialog();
            

           // var mainWindow = new MainWindow();
          //  mainWindow.DataContext = mainModel;
            
            //mainWindow.Show();
        }

        public ApplicationDbContext GetNewContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=45.12.229.46,1433;Database=mydb;User Id=sa;Password=strongPassword123;TrustServerCertificate=true;");

            var context = new ApplicationDbContext(optionsBuilder.Options);
            return context;
        }

        private void FillEfFakeData()
        {
            using(var context = GetNewContext())
            {
                context.Rebuild();
            }

            var fakeDataFactory = new FakeDataFactory();
            using (var context = GetNewContext())
            {
                var userRepository = new EfBaseRepository<User>(context);
                foreach (var user in fakeDataFactory.Users)
                {
                    userRepository.Add(user);
                }
            }


            foreach (var doctor in fakeDataFactory.Doctors)
            {
                using (var context = GetNewContext())
                {
                    var doctorRepository = new EfBaseRepository<Doctor>(context);
                    doctorRepository.Add(doctor);
                }
            }

            using (var context = GetNewContext())
            {
                var patientsRepository = new EfBaseRepository<Patient>(context);

                foreach (var patient in fakeDataFactory.Patients)
                {
                    patientsRepository.Add(patient);
                }
            }

            using (var context = GetNewContext())
            {
                var visitsRepository = new EfBaseRepository<Visit>(context);

                foreach (var visit in fakeDataFactory.Visits)
                {
                    visitsRepository.Add(visit);
                }
            }
        }

        private void FillFakeDataInMemory()
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
