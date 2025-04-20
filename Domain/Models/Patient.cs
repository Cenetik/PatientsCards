namespace Domain.Models
{
    public class Patient : Person
    {
        public virtual List<Visit> Visits { get; set;}

        public string SocialSecurityNumber { get; set; }

        public Visit LastVisit
        {
            get
            {
                if (Visits == null || Visits.Count == 0)
                {
                    return null;
                }

                return Visits.OrderBy(p => p.DateVisit).Last();                
            }
        }

        public DateTime? LastVisitDate
        {
            get
            {
                if(LastVisit== null)
                {
                    return null;
                }

                return LastVisit.DateVisit;
            }
        }

        public Doctor LastVisitedDoctor
        {
            get
            {
                if (LastVisit == null)
                {
                    return null;
                }

                return LastVisit.Doctor;
            }
        }
    }
}
