using App.Models;
using App.Services;
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
        private readonly PatientDto patient;        
        private readonly DoctorService doctorService;
        private readonly VisitsService visitsService;

        private ObservableCollection<VisitDto> visits;
        public ObservableCollection<VisitDto> Visits
        {
            get => visits; 
            set
            {
                if (visits != value)
                {
                    visits = value; OnPropertyChanged();
                }
            }
        }

        private VisitDto _selectedVisit;
        public VisitDto SelectedVisit
        {
            get => _selectedVisit;
            set
            {
                if (_selectedVisit != value)
                {
                    _selectedVisit = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddVisitCommand { get; set; }
        public ICommand EditVisitCommand { get; set; }
        public ICommand DeleteVisitCommand { get; set; }


        public List<Gender> Genders { get; } = new List<Gender> { Gender.Male, Gender.Female };

        public EditPatientCardViewModel()
        {
            this.patient = new PatientDto();
        }

        public EditPatientCardViewModel(PatientDto patient, DoctorService doctorService, VisitsService visitsService)
        {
            this.doctorService = doctorService;
            this.visitsService = visitsService;
            if (patient != null)
            {
                this.patient = patient;
            }
            else
            {
                this.patient = new PatientDto();
            }
            AddVisitCommand = new RelayCommand(AddVisit);
            EditVisitCommand = new RelayCommand(EditVisit);
            DeleteVisitCommand = new RelayCommand(DeleteVisit);

            RefreshVisits();            
        }

        private void RefreshVisits()
        {
            var visits = visitsService.GetByPatientId(patient.Id).ToList();
            Visits = new ObservableCollection<VisitDto>();

            foreach (var item in visits)
            {
                Visits.Add(item);
            }
        }

        private void DeleteVisit()
        {
            throw new NotImplementedException();
        }

        private void EditVisit()
        {
            if (SelectedVisit == null)
                return;            
            AddEditVisit(SelectedVisit.Id);            
        }

        private void AddEditVisit(Guid? selectedVisitId)
        {
            var isAdd = selectedVisitId == null;
            var visitVm = new AddEditVisitViewModel(patient, visitsService, doctorService, selectedVisitId);

            var visitWindow = new AddEditVisitWindow();
            visitVm.CloseAction = new Action(()=>
            {
                visitWindow.DialogResult = true;
                visitWindow.Close(); 
            });
            visitWindow.DataContext = visitVm;
            if (visitWindow.ShowDialog() == true)
            {
                /*if (isAdd)
                    Visits.Add(visitVm.Visit);
                else
                    MapVisit(SelectedVisit, visitVm.Visit);*/
                //RefreshView();
                RefreshVisits();
            }
            
        }

        private void MapVisit(VisitDto dest, VisitDto src)
        {
            dest.DateVisit = src.DateVisit;
            dest.Diagnosis = src.Diagnosis;
            dest.Patient = src.Patient;
            dest.Treatment = src.Treatment;
            dest.Anamnesis = src.Anamnesis;
            dest.Doctor = src.Doctor;          
            SelectedVisit = dest;
        }

        private void AddVisit()
        {
            AddEditVisit(null);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
