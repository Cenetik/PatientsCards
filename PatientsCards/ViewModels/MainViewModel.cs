using App.Models;
using App.Services;
using DataAccess.RepositoryImpls;
using Domain;
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
using System.Windows;
using System.Windows.Input;

namespace PatientsCardsUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PatientDto> patients;

        public ObservableCollection<PatientDto> Patients
        {
            get => patients;
            set
            {
                if (patients != value)
                {
                    patients = value;
                    OnPropertyChanged();
                }
            }
        }

        private DoctorService doctorService { get; set; }
        private PatientsService patientsService { get; set; }
        private VisitsService visitsService { get; set; }

        public PatientDto SelectedPatient { get; set; }
        
        public ICommand AddPatientCommand { get; set; }
        public ICommand EditPatientCommand { get; set; }
        public ICommand DeletePatientCommand { get; set; }
        public ICommand ShowPatientCardCommand { get; set; }

        public MainViewModel()
        {
            Patients = new ObservableCollection<PatientDto>();            

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            ShowPatientCardCommand = new RelayCommand(ShowPatientCard);

            var fakeDataFactory = new FakeDataFactory();
            var doctorsRepository = new InMemoryBaseRepository<Doctor>(fakeDataFactory.Doctors);
            var patientsRepository = new InMemoryBaseRepository<Patient>(fakeDataFactory.Patients);
            var visitsRepository = new InMemoryBaseRepository<Visit>(fakeDataFactory.Visits);

            doctorService = new DoctorService(doctorsRepository);
            patientsService = new PatientsService(patientsRepository,visitsRepository);
            visitsService = new VisitsService(visitsRepository,doctorsRepository,patientsRepository);
            RefreshView();
        }

        public void RefreshView()
        {
            Patients = new ObservableCollection<PatientDto>(patientsService.GetAll().OrderBy(p=>p.LastName).ToList());           
        }

        public static DateTime RandomDateOfBirth(Random rng)
        {
            DateTime startDate = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(rng.Next(range));
        }

        private void ShowPatientCard()
        {
            var patient = SelectedPatient;
            var editVm = new EditPatientCardViewModel(patient, patientsService, doctorService,visitsService);            
            var editWindow = new EditPatientCardWindow { DataContext = editVm };
            editWindow.ShowDialog();
            RefreshView();
        }

        private void AddPatient()
        {
            // Логика добавления записи
            var sel = SelectedPatient;
            var editVm = new EditPatientCardViewModel(null, patientsService, doctorService, visitsService);
            var editWindow = new EditPatientCardWindow { DataContext = editVm };
            editWindow.ShowDialog();            
            RefreshView();
        }

        private void EditPatient()
        {
            throw new NotImplementedException();
        }

        private void DeletePatient()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
