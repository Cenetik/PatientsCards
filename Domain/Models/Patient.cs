namespace Domain.Models
{
    public class Patient : Person
    {
        public List<Visit> Visits { get; set;}
    }
}
