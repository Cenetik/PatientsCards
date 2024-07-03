using App.Exceptions;
using App.Extensions;
using App.Models;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class VisitsService
    {
        private readonly IRepository<Visit> visitsRepository;
        private readonly IRepository<Doctor> doctorsRepository;
        private readonly IRepository<Patient> patientsRepository;

        public VisitsService(IRepository<Visit> visitsRepository, IRepository<Doctor> doctorsRepository, IRepository<Patient> patientsRepository)
        {
            this.visitsRepository = visitsRepository;
            this.doctorsRepository = doctorsRepository;
            this.patientsRepository = patientsRepository;
        }

        public VisitDto GetById(Guid id)
        {
            CheckIfNotExists(id);
            return visitsRepository.GetById(id).ToDto(); 
        }

        public IList<VisitDto> GetAll()
        {
            var visits = visitsRepository.GetAll();
            return visits.Select(p => p.ToDto()).ToList();
        }

        public void Delete(VisitDto visitDto)
        {
            CheckIfNotExists(visitDto.Id);

            var patient = visitsRepository.GetById(visitDto.Id);

            visitsRepository.Delete(patient);
        }

        public void Edit(VisitDto visitDto)
        {
            ValidateFields(visitDto);

            CheckIfNotExists(visitDto.Id);

            var visit = visitsRepository.GetById(visitDto.Id);

            Doctor doctor = null;
            if(visitDto.Doctor != null)
                doctor = doctorsRepository.GetById(visitDto.Doctor.Id);
            Patient patient = null;
            if(visitDto.Patient != null)
                patient = patientsRepository.GetById(visitDto.Patient.Id);

            visit.DateVisit = visitDto.DateVisit;
            visit.Diagnosis = visitDto.Diagnosis;
            visit.Anamnesis = visitDto.Anamnesis;
            visit.Treatment = visitDto.Treatment;
            visit.Doctor = doctor;
            visit.Patient = patient;

            visitsRepository.Update(visit);
        }

        public void Add(VisitDto visitDto)
        {
            ValidateFields(visitDto);

            Doctor doctor = null;
            if (visitDto.Doctor != null)
                doctor = doctorsRepository.GetById(visitDto.Doctor.Id);
            Patient patient = null;
            if (visitDto.Patient != null)
                patient = patientsRepository.GetById(visitDto.Patient.Id);

            var visit = new Visit();
            visit.DateVisit = visitDto.DateVisit;
            visit.Diagnosis = visitDto.Diagnosis;
            visit.Anamnesis = visitDto.Anamnesis;
            visit.Treatment = visitDto.Treatment;
            visit.Doctor = doctor;
            visit.Patient = patient;

            Guid id = visitsRepository.Add(visit);
            visitDto.Id = id;
        }

        private void CheckIfNotExists(Guid visitId)
        {
            var exists = visitsRepository.GetAll(p => p.Id == visitId).ToList();
            if (exists.Count() == 0)
                throw new ValidateException("Не существует визита с ID " + visitId);
        }

        private void ValidateFields(VisitDto visit)
        {
            if (visit.DateVisit == default(DateTime))
                throw new ValidateException("Не указана дата визита!");

            if (visit.Patient == null)
                throw new ValidateException("Не указан пациент!");

            if (visit.Doctor == null)
                throw new ValidateException("Не указан врач!");

            if (string.IsNullOrEmpty(visit.Anamnesis))
                throw new ValidateException("Не указан анамнез!");

            if (string.IsNullOrEmpty(visit.Treatment))
                throw new ValidateException("Не указано лечение!");

            if (string.IsNullOrEmpty(visit.Diagnosis))
                throw new ValidateException("Не указано диагноз!");
        }

        public IList<VisitDto> GetByPatientId(Guid patientId)
        {
            var visits = visitsRepository.GetAll(p => p.Patient.Id == patientId);
            return visits.Select(p => p.ToDto()).ToList();            
        }
    }
}
