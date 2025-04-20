
namespace Domain.Models
{
    public class Doctor : Person
    {
        public string Qualification { get; set; }
        public virtual List<Visit> Visits { get; set; }        

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic} ({Qualification})";
            //return LastName + " " + FirstName + " " + Patronymic;
        }
    }
}
