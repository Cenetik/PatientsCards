using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FakeDataFactory
    {
        public readonly List<Doctor> Doctors;
        public readonly List<Patient> Patients;
        public readonly List<Visit> Visits;

        public FakeDataFactory()
        {
            var names = new[] { "Иван", "Сергей", "Александр", "Алексей", "Фёдор" };
            var patronymics = new[] { "Иванович", "Сергеевич", "Александрович", "Алексеевич", "Фёдорович" };
            var lastnames = new[] { "Измайлов", "Конюхов", "Сипунов", "Горохов", "Синюков", "Афанасьев" };
            var qualifications = new[] { "Терапевт", "Хирург", "Гастроэнтеролог", "Стоматолог", "Патологоанатом" };

            var testDoctors = new Faker<Doctor>()
                .StrictMode(true)
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(10, new DateTime(1980, 1, 1)))
                .RuleFor(p => p.FirstName, f => f.PickRandom(names))
                .RuleFor(p => p.Patronymic, f => f.PickRandom(patronymics))
                .RuleFor(p => p.LastName, f => f.PickRandom(lastnames))
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Gender, f => (int)Gender.Male)
                .RuleFor(u => u.Qualification, f => f.PickRandom(qualifications))
                .RuleFor(u => u.Visits, f => new List<Visit>());

            Doctors = testDoctors.Generate(5);

            names = new[] { "Ирина", "Елена", "Ольга", "Анна", "Екатерина", "Евгения", "Анастасия" };
            patronymics = new[] { "Ивановна", "Сергеевна", "Александровна", "Алексеевна", "Фёдоровна", "Семёновна" };
            lastnames = new[] { "Кукушкина", "Городецкая", "Клюкова", "Дорохович", "Плинтус", "Катакомба", "Кукумбер" };
            var socialNumbers = new[]
            {
                "2222554892722",
                "6t46464353467",
                "2556756787686",
                "8575643457755",
                "2876823435435",
                "8995463522354",
                "7343556765588",
                "5387686746335",
                "0075745463433",
                "2353475674466",
            };

            var testPatients = new Faker<Patient>()
                .StrictMode(true)
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(10, new DateTime(1980, 1, 1)))
                .RuleFor(p => p.FirstName, f => f.PickRandom(names))
                .RuleFor(p => p.Patronymic, f => f.PickRandom(patronymics))
                .RuleFor(p => p.LastName, f => f.PickRandom(lastnames))
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Gender, f => (int)Gender.Female)  
                .RuleFor(u=>u.SocialSecurityNumber,f=>f.PickRandom(socialNumbers))
                .RuleFor(u => u.Visits, f => new List<Visit>());

            Patients = testPatients.Generate(10);

            names = new[] { "Иван", "Сергей", "Игорь", "Николай", "Борис", "Леонид", "Семён" };
            patronymics = new[] { "Иванович", "Сергеевич", "Александрович", "Алексеевич", "Фёдорович", "Семёнович" };
            lastnames = new[] { "Клюквин", "Буркатовский", "Бритва", "Кузнецов", "Карась", "Тараканов", "Дихлофосов" };
            socialNumbers = new[]
            {
                "9988548751164",
                "6456434534564",
                "3767563534555",
                "7657534534534",
                "5855635437856",
                "0856763432674",
                "9685643458754",
                "9574635235788",
                "9676562431574",
                "9764526262254",
            };

            testPatients = new Faker<Patient>()
                .StrictMode(true)
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(10, new DateTime(1980, 1, 1)))
                .RuleFor(p => p.FirstName, f => f.PickRandom(names))
                .RuleFor(p => p.Patronymic, f => f.PickRandom(patronymics))
                .RuleFor(p => p.LastName, f => f.PickRandom(lastnames))
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Gender, f => (int)Gender.Male)
                .RuleFor(u => u.SocialSecurityNumber, f => f.PickRandom(socialNumbers))
                .RuleFor(u => u.Visits, f => new List<Visit>());

            Patients.AddRange(testPatients.Generate(10));

            var anamnesises = new[] { "жалобы на боль в груди после физических нагрузок.",
                                      "жалуется на головокружения и слабость.",
                                      "обратился со спазмами в животе.",
                                      "сталкивается с одышкой и учащенным сердцебиением.",
                                      "жалобы на частые головные боли.",
                                      "жалуется на сыпь на коже и зуд.",
                                      "жалобы на боли в спине и ногах.",
                                      "страдает от постоянной усталости и снижения энергии.",
                                      "проблемы с пищеварением",
                                      "необъяснимая потеря веса"
                                      };
            var treatments = new[]
            {
                "Прописан курс антибиотиков",
                "Провести кардио-реабилитацию",
                "Прописаны противовоспалительные препараты",
                "Провести серию массажных процедур",
                "Пить больше воды и молиться",
                "Прописаны препараты от аллергии",
                "Провести дополнительное обследование для уточнения диагноза.",
                "Назначить курс физиотерапии",
            };

            var diagnosis = new[]
            {
                "Ишемическая болезнь сердца.",
                "Мигрень.",
                "Гастрит.",
                "Артрит.",
                "Аллергический дерматит.",
                "Остеохондроз позвоночника.",
                "Депрессия.",
                "Гипертония.",
                "Сахарный диабет.",
                "Острый аппендицит."
            };

            var testVisits = new Faker<Visit>()
                .StrictMode(true)
                .RuleFor(p => p.Anamnesis, f => f.PickRandom(anamnesises))
                .RuleFor(p => p.Diagnosis, f => f.PickRandom(diagnosis))
                .RuleFor(p => p.Treatment, f => f.PickRandom(treatments))
                .RuleFor(p => p.Doctor, f => f.PickRandom(Doctors))
                .RuleFor(p => p.Patient, f => f.PickRandom(Patients))
                .RuleFor(p => p.DateVisit, f => f.Date.Past(1, DateTime.Now))
                .RuleFor(u => u.Id, f => Guid.NewGuid());

            Visits = testVisits.Generate(20);

            foreach (var v in Visits)
            {
                v.Doctor.Visits.Add(v);
                v.Patient.Visits.Add(v);
            }
        }     
    }
}
