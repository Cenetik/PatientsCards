using System;
using System.Configuration;
using System.Data;
using System.Windows;
using App.Exceptions;
using PatientsCards;

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


            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            //var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
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
