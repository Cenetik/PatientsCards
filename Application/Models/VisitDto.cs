using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class VisitDto
    {
        public Guid Id { get; set; }

        public DateTime DateVisit { get; set; }

        public string Diagnosis { get; set; }

        public string Anamnesis { get; set; }

        public DoctorDto Doctor { get; set; }

        public PatientDto Patient { get; set; }

        public string Treatment { get; set; }

        
    }
}
