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

       /* public MainViewModel() 
        {
            Patients = new ObservableCollection<PatientDto>();

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            ShowPatientCardCommand = new RelayCommand(ShowPatientCard);

            var doctorRepo = new InMemoryBaseRepository<Doctor>(new List<Doctor>());
            var patientsRepo = new InMemoryBaseRepository<Patient>(new List<Patient>());
            var visitsRepo = new InMemoryBaseRepository<Visit>(new List<Visit>());

            this.doctorService = new DoctorService(doctorRepo);
            this.patientsService = new PatientsService(patientsRepo, visitsRepo);
            this.visitsService = new VisitsService(visitsRepo,doctorRepo,patientsRepo);
        }

        public MainViewModel(DoctorService doctorService, PatientsService patientsService, VisitsService visitsService)
        {
            Patients = new ObservableCollection<PatientDto>();            

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            ShowPatientCardCommand = new RelayCommand(ShowPatientCard);

            this.doctorService = doctorService;
            this.patientsService = patientsService;
            this.visitsService = visitsService;

            RefreshView();            
        }*/

        public MainViewModel()
        {
           //var fakeDataFactory = new FakeDataFactory();
            //var doctorsRepo = new InMemoryBaseRepository<Doctor>(fakeDataFactory.Doctors);
            //var patientsRepo = new InMemoryBaseRepository<Patient>(fakeDataFactory.Patients);
            //var visitsRepo = new InMemoryBaseRepository<Visit>(fakeDataFactory.Visits);
            //var usersRepo = new InMemoryBaseRepository<User>(fakeDataFactory.Users);
            var doctorsRepo = new EfBaseRepository<Doctor>(GlobalData.Context);
            var patientsRepo = new EfBaseRepository<Patient>(GlobalData.Context);
            var visitsRepo = new EfBaseRepository<Visit>(GlobalData.Context);
            var usersRepo = new EfBaseRepository<User>(GlobalData.Context);

            this.doctorService = new DoctorService(doctorsRepo);
            this.patientsService = new PatientsService(patientsRepo, visitsRepo, doctorsRepo);
            this.visitsService = new VisitsService(visitsRepo, doctorsRepo, patientsRepo);

            Patients = new ObservableCollection<PatientDto>();

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            ShowPatientCardCommand = new RelayCommand(ShowPatientCard);

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
