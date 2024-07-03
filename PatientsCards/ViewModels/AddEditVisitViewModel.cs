using App.Models;
using App.Services;
using App.Exceptions;
using DataAccess.RepositoryImpls;
using Domain.Models;
using Domain.Repositories;
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
    public class AddEditVisitViewModel : INotifyPropertyChanged
    {
        public readonly VisitDto Visit;
        private readonly VisitsService visitsService;
        private readonly DoctorService doctorService;
        private Action closeAction;

        public ICommand SaveVisitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public List<DoctorDto> Doctors { get; set; }

        public AddEditVisitViewModel()
        {
            this.Visit = new VisitDto();
        }

        public AddEditVisitViewModel(PatientDto patient, VisitsService visitsService, DoctorService doctorService, Guid? visitId)
        {
            this.visitsService = visitsService;
            this.doctorService = doctorService;
            if (visitId == null)
            {
                this.Visit = new VisitDto
                {
                    Patient = patient,
                    DateVisit = DateTime.Now,
                };
            }
            else
            {
                this.Visit = visitsService.GetById(visitId.Value);
            }

            SaveVisitCommand = new RelayCommand(SaveVisit);
            CancelCommand = new RelayCommand(Cancel);
                    
            // Сюда бы воткнуть репозиторий
            Doctors = doctorService.GetAll().ToList();            
        }

        public void SaveVisit()
        {
            // сохраняем в БД
            if (Visit.Id != Guid.Empty)
            {
                visitsService.Edit(Visit);
            }
            else
            {
                visitsService.Add(Visit);
            }

            if (CloseAction != null)
                CloseAction.Invoke();
        }      

        public void Cancel()
        {

        }

        public AddEditVisitViewModel(VisitDto visit)
        {
            this.Visit = visit;
        }

        public DoctorDto Doctor
        {
            get => Visit.Doctor; 
            set
            {
                Visit.Doctor = value;
                OnPropertyChanged();
            }
        }

        public PatientDto Patient
        {
            get=> Visit.Patient;
        }

        public DateTime DateVisit
        {
            get => Visit.DateVisit;
            set
            {
                Visit.DateVisit = value;
                OnPropertyChanged();
            }
        }

        public string Diagnosis
        {
            get=> Visit.Diagnosis;
            set
            {
                Visit.Diagnosis = value;
                OnPropertyChanged();
            }
        }

        public string Anamnesis
        {
            get => Visit.Anamnesis;
            set
            {
                Visit.Anamnesis = value;
                OnPropertyChanged();
            }
        }

        public string Treatment
        {
            get => Visit.Treatment;
            set
            {
                Visit.Treatment = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction 
        { 
            get => closeAction; 
            set => closeAction = value; 
        }        

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
