using App.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class DtoExtensions
    {
        public static DoctorDto ToDto(this Doctor doctor)
        {
            return new DoctorDto
            {
                DateOfBirth = doctor.DateOfBirth,
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = (Gender)doctor.Gender,
                Patronymic = doctor.Patronymic,
                Qualification = doctor.Qualification
            };
        }

        public static PatientDto ToDto(this Patient patient)
        {
            DateTime? lastVisitDate = null;
            DoctorDto lastVisitDoctor = null;           

            return new PatientDto
            {
                DateOfBirth = patient.DateOfBirth,
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = (Gender)patient.Gender,
                Patronymic = patient.Patronymic,
                SocialSecurityNumber = patient.SocialSecurityNumber
                
            };
        }

        public static VisitDto ToDto(this Visit visit)
        {
            DoctorDto doctorDto = null;
            if(visit.Doctor!= null)
            {
                doctorDto = visit.Doctor.ToDto();
            }

            PatientDto patientDto = null;
            if (visit.Doctor != null)
            {
                patientDto = visit.Patient.ToDto();
            }

            return new VisitDto
            {
               Id = visit.Id,
               Anamnesis = visit.Anamnesis,
               DateVisit = visit.DateVisit,
               Diagnosis = visit.Diagnosis,   
               Doctor = doctorDto,
               Patient = patientDto,
               Treatment = visit.Treatment
            };
        }
    }
}
