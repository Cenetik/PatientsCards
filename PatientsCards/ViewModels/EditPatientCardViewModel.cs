using Domain.Models;
using PatientsCardsUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientsCardsUI.ViewModels
{
    public class EditPatientCardViewModel : INotifyPropertyChanged
    {
        private readonly Person person;

        public ICommand AddVisitCommand { get; set; }
        public ICommand EditVisitCommand { get; set; }
        public ICommand DeleteVisitCommand { get; set; }

        public EditPatientCardViewModel()
        {
            this.person = new Person();
        }

        public EditPatientCardViewModel(Person person)
        {
            if (person != null)
            {
                this.person = person;
            }
            else
            {
                this.person = new Person();
            }
            AddVisitCommand = new RelayCommand(AddVisit);
            EditVisitCommand = new RelayCommand(EditVisit);
            DeleteVisitCommand = new RelayCommand(DeleteVisit);
        }

        private void DeleteVisit()
        {
            throw new NotImplementedException();
        }

        private void EditVisit()
        {
            throw new NotImplementedException();
        }

        private void AddVisit()
        {
            throw new NotImplementedException();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string FirstName
        {
            get => person.FirstName;
            set
            {
                person.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic
        {
            get => person.Patronymic;
            set
            {
                person.Patronymic = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                person.LastName = value;
                OnPropertyChanged();
            }
        }

        public Gender Gender
        {
            get => person.Gender;
            set
            {
                person.Gender = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => person.DateOfBirth;
            set
            {
                person.DateOfBirth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Age)); // Update Age when DateOfBirth changes
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                // Go back to the year the person was born in case of a leap year
                if (DateOfBirth > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
