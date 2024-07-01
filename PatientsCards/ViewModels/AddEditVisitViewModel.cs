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
        private readonly Visit visit;
        private readonly IRepository<Visit> visitsRepository;

        public ICommand SaveVisitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public List<Doctor> Doctors { get; set; }

        public AddEditVisitViewModel()
        {
            this.visit = new Visit();
        }

        public AddEditVisitViewModel(Patient patient, IRepository<Visit> visitsRepository, IRepository<Doctor> doctorsRepository)
        {
            this.visit = new Visit
            {
                Patient = patient,
                DateVisit = DateTime.Now,
            };

            SaveVisitCommand = new RelayCommand(SaveVisit);
            CancelCommand = new RelayCommand(Cancel);
                    
            // Сюда бы воткнуть репозиторий
            Doctors = doctorsRepository.GetAll().ToList();
            this.visitsRepository = visitsRepository;
        }

        public void SaveVisit()
        {
            // сохраняем в БД
            throw new NotImplementedException();
        }

        public void Cancel()
        {

        }

        public AddEditVisitViewModel(Visit visit)
        {
            this.visit = visit;
        }

        public Doctor Doctor
        {
            get => visit.Doctor; 
            set
            {
                visit.Doctor = value;
                OnPropertyChanged();
            }
        }

        public Patient Patient
        {
            get=>visit.Patient;
        }

        public DateTime DateVisit
        {
            get => visit.DateVisit;
            set
            {
                visit.DateVisit = value;
                OnPropertyChanged();
            }
        }

        public string Diagnosis
        {
            get=>visit.Diagnosis;
            set
            {
                visit.Diagnosis = value;
                OnPropertyChanged();
            }
        }

        public string Anamnesis
        {
            get => visit.Anamnesis;
            set
            {
                visit.Anamnesis = value;
                OnPropertyChanged();
            }
        }

        public string Treatment
        {
            get => visit.Treatment;
            set
            {
                visit.Treatment = value;
                OnPropertyChanged();
            }
        }        

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
