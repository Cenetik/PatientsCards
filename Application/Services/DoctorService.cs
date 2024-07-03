using App.Extensions;
using App.Models;
using App.Exceptions;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class DoctorService
    {
        private readonly IRepository<Doctor> doctorsRepository;

        public DoctorService(IRepository<Doctor> doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public DoctorDto GetById(Guid id)
        {
            CheckIfNotExists(id);
            return doctorsRepository.GetById(id).ToDto();
        }

        public IList<DoctorDto> GetAll()
        {
            var doctors = doctorsRepository.GetAll();
            return doctors.Select(p=>p.ToDto()).ToList();
        }        

        public void Delete(DoctorDto dto)
        {
            CheckIfNotExists(dto.Id);

            var doctor = doctorsRepository.GetById(dto.Id);

            doctorsRepository.Delete(doctor);
        }

        public void Edit(DoctorDto doctorDto)
        {
            ValidateFields(doctorDto);

            CheckIfNotExists(doctorDto.Id);

            var doctor = doctorsRepository.GetById(doctorDto.Id);

            doctor.DateOfBirth = doctorDto.DateOfBirth;
            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Patronymic = doctorDto.Patronymic;
            doctor.Qualification = doctorDto.Qualification;
            doctor.Gender = (int)doctorDto.Gender;               

            doctorsRepository.Add(doctor);
        }

        public void Add(DoctorDto doctorDto)
        {
            ValidateFields(doctorDto);            

            var doctor = new Doctor()
            {
                DateOfBirth = doctorDto.DateOfBirth,
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Gender = (int)doctorDto.Gender,
                Patronymic = doctorDto.Patronymic,
                Qualification = doctorDto.Qualification,
            };

            doctorsRepository.Add(doctor);
        }        

        private void CheckIfNotExists(Guid doctorId)
        {
            var exists = doctorsRepository.GetAll(p=>p.Id==doctorId).ToList();
            if (exists.Count() == 0 )
                throw new ValidateException("Не существует врача с идентификатором "+doctorId);
        }        

        private void ValidateFields(DoctorDto doctor)
        {
            if (string.IsNullOrEmpty(doctor.Qualification))
                throw new ValidateException("Не указана квалификация врача!");
            if (string.IsNullOrEmpty(doctor.FirstName))
                throw new ValidateException("Не указано имя!");
            if (string.IsNullOrEmpty(doctor.LastName))
                throw new ValidateException("Не указана фамилия!");
            if (string.IsNullOrEmpty(doctor.Patronymic))
                throw new ValidateException("Не указано отчество!");            
        }
    }
}
