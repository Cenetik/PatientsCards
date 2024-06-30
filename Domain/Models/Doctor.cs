namespace Domain.Models
{
    public class Doctor : Person
    {
        public string Qualification { get; set; }

        public List<Visit> Visits { get;set;}
    }
}
