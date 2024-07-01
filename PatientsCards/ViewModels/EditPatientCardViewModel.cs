using Domain.Models;
using Domain.Repositories;
using PatientsCardsUI.Commands;
using PatientsCardsUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly Patient patient;        
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Visit> visitRepository;

        public ObservableCollection<Visit> Visits { get; private set; }
        public Visit SelectedVisit { get; set; }

        public ICommand AddVisitCommand { get; set; }
        public ICommand EditVisitCommand { get; set; }
        public ICommand DeleteVisitCommand { get; set; }


        public List<Gender> Genders { get; } = new List<Gender> { Gender.Male, Gender.Female };

        public EditPatientCardViewModel()
        {
            this.patient = new Patient();
        }

        public EditPatientCardViewModel(Patient patient, IRepository<Doctor> doctorRepository, IRepository<Visit> visitRepository)
        {
            if (patient != null)
            {
                this.patient = patient;
            }
            else
            {
                this.patient = new Patient();
            }
            AddVisitCommand = new RelayCommand(AddVisit);
            EditVisitCommand = new RelayCommand(EditVisit);
            DeleteVisitCommand = new RelayCommand(DeleteVisit);            
            this.doctorRepository = doctorRepository;
            this.visitRepository = visitRepository;

            Visits = new ObservableCollection<Visit>(visitRepository.GetAll(p => p.Patient.Id == patient.Id).ToList());
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
            var visitVm = new AddEditVisitViewModel(patient, visitRepository,doctorRepository);
            var visitWindow = new AddEditVisitWindow { DataContext = visitVm };
            if (visitWindow.ShowDialog() == true)
            {
                //SelectedItem.Name = editVm.Item.Name; // Обновляем данные, если пользователь нажал OK
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string FirstName
        {
            get => patient.FirstName;
            set
            {
                patient.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic
        {
            get => patient.Patronymic;
            set
            {
                patient.Patronymic = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => patient.LastName;
            set
            {
                patient.LastName = value;
                OnPropertyChanged();
            }
        }

        public Gender Gender
        {
            get => patient.Gender;
            set
            {
                patient.Gender = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => patient.DateOfBirth;
            set
            {
                patient.DateOfBirth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Age)); 
            }
        }

        public int Age
        {
            get => patient.Age;            
        }
    }
}
