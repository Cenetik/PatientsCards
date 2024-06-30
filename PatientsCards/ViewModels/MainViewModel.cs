using Domain.Models;
using PatientsCardsUI.Commands;
using PatientsCardsUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientsCardsUI.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }

        public Patient SelectedPatient { get; set; }
        
        public ICommand AddPatientCommand { get; set; }
        public ICommand EditPatientCommand { get; set; }
        public ICommand DeletePatientCommand { get; set; }
        public ICommand ShowPatientCardCommand { get; set; }

        public MainViewModel()
        {
            Patients = new ObservableCollection<Patient>();

            RefreshView();

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            ShowPatientCardCommand = new RelayCommand(ShowPatientCard);
        }

        public void RefreshView()
        {
            Random rng = new Random();

            // Создаем имена, фамилии и отчества для случайной генерации
            string[] firstNames = { "Иван", "Алексей", "Мария", "Елена", "Сергей", "Дарья", "Николай", "Владимир", "Анна", "Ольга" };
            string[] lastNames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов" };
            string[] patronymics = { "Алексеевич", "Иванович", "Дмитриевна", "Олегович", "Николаевич", "Константиновна", "Владимирович", "Петрович", "Сергеевна", "Викторович" };

            for (int i = 0; i < 10; i++)
            {
                Patients.Add(new Patient
                {
                    FirstName = firstNames[rng.Next(firstNames.Length)],
                    LastName = lastNames[rng.Next(lastNames.Length)],
                    Patronymic = patronymics[rng.Next(patronymics.Length)],
                    Gender = (Gender)rng.Next(2),  // случайно выбираем Мужской или Женский
                    DateOfBirth = RandomDateOfBirth(rng)
                });
            }
        }

        public static DateTime RandomDateOfBirth(Random rng)
        {
            DateTime startDate = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(rng.Next(range));
        }

        private void ShowPatientCard()
        {
            var sel = SelectedPatient;
            var editVm = new EditPatientCardViewModel(sel);
            var editWindow = new EditPatientCardWindow { DataContext = editVm };
            if (editWindow.ShowDialog() == true)
            {
                //SelectedItem.Name = editVm.Item.Name; // Обновляем данные, если пользователь нажал OK
            }
        }

        private void AddPatient()
        {
            // Логика добавления записи
            throw new NotImplementedException();
        }

        private void EditPatient()
        {
            throw new NotImplementedException();
        }

        private void DeletePatient()
        {
            throw new NotImplementedException();
        }
    }
}
