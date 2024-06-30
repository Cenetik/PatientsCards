using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum Gender
    {
        Male,
        Female        
    }

    public class Person : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                if (DateOfBirth > today.AddYears(-age)) age--;
                return age;
            }
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic}";
            //return LastName + " " + FirstName + " " + Patronymic;
        }
    }
}
