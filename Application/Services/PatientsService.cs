using App.Extensions;
using App.Models;
using App.Exceptions;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class PatientsService
    {
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<Doctor> doctorsRepository;
        private readonly IRepository<Visit> visitsRepository;

        public PatientsService(IRepository<Patient> patientsRepository, IRepository<Visit> visitsRepository, IRepository<Doctor> doctorRepository)
        {
            this.patientsRepository = patientsRepository;
            this.visitsRepository = visitsRepository;
            this.doctorsRepository = doctorRepository;
        }

        public PatientDto GetById(Guid id)
        {
            CheckIfNotExists(id);
            var patient = patientsRepository.GetById(id).ToDto();
            SetLastVisitData(patient);
            return patient;
        }

        public IList<PatientDto> GetAll()
        {
            var patients = patientsRepository.GetAll();
            var patientsDtos = patients.Select(p => p.ToDto()).ToList();
            foreach (var patient in patientsDtos)
            {
                SetLastVisitData(patient);                
            }
            return patientsDtos;
        }

        private void SetLastVisitData(PatientDto patient)
        {
            var visits = visitsRepository.GetAll(p => p.Patient.Id == patient.Id).ToList();
            if (visits.Count > 0)
            {
                var lastVisit = visits.OrderBy(p => p.DateVisit).Last();
                patient.LastVisitDate = lastVisit.DateVisit;
                var doctor = doctorsRepository.GetById(lastVisit.DoctorId).ToDto();
                patient.LastVisitedDoctor = doctor;// lastVisit.Doctor.ToDto();
            }
        }

        public void Delete(PatientDto dto)
        {
            CheckIfNotExists(dto.Id);

            var patient = patientsRepository.GetById(dto.Id);

            patientsRepository.Delete(patient);
        }

        public void Edit(PatientDto patientDto)
        {
            ValidateFields(patientDto);

            CheckIfNotExists(patientDto.Id);

            var patient = patientsRepository.GetById(patientDto.Id);

            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.Patronymic = patientDto.Patronymic;
            patient.SocialSecurityNumber = patientDto.SocialSecurityNumber;
            patient.Gender = (int)patientDto.Gender;

            patientsRepository.Update(patient);
        }

        public void Add(PatientDto patientDto)
        {
            ValidateFields(patientDto);

            var patient = new Patient()
            {
                DateOfBirth = patientDto.DateOfBirth,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Gender = (int)patientDto.Gender,
                Patronymic = patientDto.Patronymic,
                SocialSecurityNumber = patientDto.SocialSecurityNumber,
            };

            patientsRepository.Add(patient);
        }

        private void CheckIfNotExists(Guid patientId)
        {
            var exists = patientsRepository.GetAll(p => p.Id == patientId).ToList();
            if (exists.Count() == 0)
                throw new ValidateException("Не существует врача с идентификатором " + patientId);
        }

        private void ValidateFields(PatientDto patient)
        {            
            if (string.IsNullOrEmpty(patient.FirstName))
                throw new ValidateException("Не указано имя!");
            if (string.IsNullOrEmpty(patient.LastName))
                throw new ValidateException("Не указана фамилия!");
            if (string.IsNullOrEmpty(patient.Patronymic))
                throw new ValidateException("Не указано отчество!");
            if (string.IsNullOrEmpty(patient.SocialSecurityNumber))
                throw new ValidateException("Не указан номер полиса!");

            if (patient.DateOfBirth > DateTime.Now.Date)
                throw new ValidateException("Дата рождения не может быть больше текущей!");

            if(patient.DateOfBirth == default(DateTime))
                throw new ValidateException("Не указана дата рождение");

            if (patient.Age>150)
                throw new ValidateException($"Человеку слишком много лет: {patient.Age}. Закапывайте, лечению не подлежит.");

            patient.SocialSecurityNumber = patient.SocialSecurityNumber.Trim();
            if(!patient.SocialSecurityNumber.IsNumeric())
                throw new ValidateException("Номер полиса содержит недопустимые символы! Допустимы только цифры без пробелов!");
        }
    }
}
