using Domain.Models;
using PatientsCardsUI.Commands;
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
        public ObservableCollection<Person> Persons { get; set; }

        public Person SelectedPerson { get; set; }
        
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public MainViewModel()
        {
            Persons = new ObservableCollection<Person>();
                  
            
            Random rng = new Random();

            // Создаем имена, фамилии и отчества для случайной генерации
            string[] firstNames = { "Иван", "Алексей", "Мария", "Елена", "Сергей", "Дарья", "Николай", "Владимир", "Анна", "Ольга" };
            string[] lastNames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов" };
            string[] patronymics = { "Алексеевич", "Иванович", "Дмитриевна", "Олегович", "Николаевич", "Константиновна", "Владимирович", "Петрович", "Сергеевна", "Викторович" };

            for (int i = 0; i < 10; i++)
            {
                Persons.Add(new Person
                {
                    FirstName = firstNames[rng.Next(firstNames.Length)],
                    LastName = lastNames[rng.Next(lastNames.Length)],
                    Patronymic = patronymics[rng.Next(patronymics.Length)],
                    Gender = (Gender)rng.Next(2),  // случайно выбираем Мужской или Женский
                    DateOfBirth = RandomDateOfBirth(rng)
                });
            }

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
        }

        public static DateTime RandomDateOfBirth(Random rng)
        {
            DateTime startDate = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(rng.Next(range));
        }

        private void EditPerson()
        {
          /*  var editVm = new EditPatientCardViewModel(SelectedItem);
            var editWindow = new EditItemWindow { DataContext = editVm };
            if (editWindow.ShowDialog() == true)
            {
                SelectedItem.Name = editVm.Item.Name; // Обновляем данные, если пользователь нажал OK
            }*/
        }

        private void Add()
        {
            // Логика добавления записи
        }

        private void Edit()
        {
            var sel = SelectedPerson;

        }

        private void Delete()
        {
            // Логика удаления записи
        }
    }
}
