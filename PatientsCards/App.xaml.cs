using PatientsCards;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PatientsCardsUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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
            var mainWindow = new MainWindow();


            //var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
