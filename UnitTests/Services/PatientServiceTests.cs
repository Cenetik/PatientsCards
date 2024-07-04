using App.Exceptions;
using App.Extensions;
using App.Models;
using App.Services;
using DataAccess.RepositoryImpls;
using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class PatientServiceTests
    {     
        [Fact]
        public void SavePatient_SocNumberNotNumberic_ThrowExceptioin()
        {
            var fakeDataFactory = new FakeDataFactory();
            var patientRepository = new InMemoryBaseRepository<Patient>(fakeDataFactory.Patients);
            var visitRepository = new InMemoryBaseRepository<Visit>(fakeDataFactory.Visits);
            var patientsService = new PatientsService(patientRepository,visitRepository);

            var patient = fakeDataFactory.Patients.First().ToDto();

            patient.SocialSecurityNumber = "2222Y1111";

            var exception = Assert.Throws<ValidateException>(() => patientsService.Edit(patient));
            Assert.Equal("Номер полиса содержит недопустимые символы! Допустимы только цифры без пробелов!", exception.Message);
        }
    }
}
