using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Visit : BaseEntity
    {
        public DateTime DateVisit { get; set; }

        public string Diagnosis { get; set; }
        
        public string Anamnesis { get; set; }
        
        public Guid DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }

        public Guid PatientId { get; set; }
        public virtual Patient? Patient { get; set; }        
        
        public string Treatment { get; set; }   
    }    
}
