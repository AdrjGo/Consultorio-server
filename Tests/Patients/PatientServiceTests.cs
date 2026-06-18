using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Consultorio.Tests.Patients
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepo = new();
        private readonly PatientsService _service;

        public PatientServiceTests()
        {
            _service = new PatientsService(_patientRepo.Object);
        }

        [Fact]
        public async Task GetPatientById_ShouldReturnPatientResponse_WhenPatientExists()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DateTime.UtcNow,
                Name = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
                Sex = Gender.MALE,
                Ci = "ci",
                Phone = new PhoneNumber("123")
            };

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DateTime.UtcNow,
                Person = person,
                PersonId = person.Id,
                Address = "addr",
                Zone = "z",
                City = "c",
                HomePhone = new PhoneNumber("123"),
                Occupation = "occ",
                PlaceOccupation = "place"
            };

            _patientRepo.Setup(r => r.GetPatientById(patient.Id)).ReturnsAsync(patient);

            var res = await _service.GetPatientById(patient.Id);

            Assert.Equal(patient.Id, res.Id);
            Assert.Equal(patient.Address, res.Address);
            Assert.Equal(patient.Person.Name, res.PatientPerson.Name);
        }

        [Fact]
        public async Task GetPatientById_ShouldThrow_WhenNotFound()
        {
            _patientRepo.Setup(r => r.GetPatientById(It.IsAny<Guid>())).ReturnsAsync((Patient?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetPatientById(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetPatientByName_ShouldReturnList_WhenFound()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Name = "Alice",
                LastName = "Smith",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)),
                Sex = Gender.FEMALE,
                Ci = "ci2",
                Phone = new PhoneNumber("456")
            };

            var p = new Patient
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                Person = person,
                PersonId = person.Id
            };

            _patientRepo.Setup(r => r.GetPatientsByName("Alice")).ReturnsAsync(new List<Patient?> { p });

            var list = (await _service.GetPatientByName("Alice")).ToList();

            Assert.Single(list);
            Assert.Equal(p.Id, list[0].Id);
            Assert.Equal(p.Person.Name, list[0].PatientPerson.Name);
        }

        [Fact]
        public async Task GetPagedPatients_ShouldReturnPagedResult()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Name = "A",
                LastName = "A",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Sex = Gender.MALE,
                Ci = "1",
                Phone = new PhoneNumber("1")
            };

            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = Guid.NewGuid(),
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    Person = person,
                    PersonId = person.Id,
                    Address = "addr",
                    Zone = "z",
                    City = "c",
                    Occupation = "occ",
                    PlaceOccupation = "place"
                }
            };

            _patientRepo.Setup(r => r.GetPatientsPagedAsync(1,10,null,null)).ReturnsAsync((patients, patients.Count));

            var res = await _service.GetPagedPatients(1,10);

            Assert.Equal(1, res.TotalCount);
            Assert.Single(res.Items);
        }

        [Fact]
        public async Task GetAllPatients_ShouldReturnAll()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Name = "B",
                LastName = "B",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-40)),
                Sex = Gender.FEMALE,
                Ci = "2",
                Phone = new PhoneNumber("2")
            };

            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = Guid.NewGuid(),
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    Person = person,
                    PersonId = person.Id,
                    Address = "addr",
                    Zone = "z",
                    City = "c",
                    Occupation = "occ",
                    PlaceOccupation = "place"
                }
            };

            _patientRepo.Setup(r => r.GetAllPatients()).ReturnsAsync(patients);

            var res = (await _service.GetAllPatients()).ToList();

            Assert.Single(res);
            Assert.Equal(patients[0].Id, res[0].Id);
        }

        [Fact]
        public async Task CreatePatient_ShouldCreate_WhenNotExists()
        {
            var dto = new Application.Dto.PatientDto
            {
                Address = "a",
                Zone = "z",
                City = "c",
                HomePhone = "123",
                Occupation = "occ",
                PlaceOccupation = "place",
                Person = new Application.Dto.PersonDto
                {
                    Name = "New",
                    LastName = "User",
                    BirthDate = DateTime.UtcNow.AddYears(-25).ToString("yyyy/MM/dd"),
                    Sex = "MALE",
                    Ci = "ci3",
                    Email = "e@e.com",
                    Phone = "789",
                    Profession = "prof"
                }
            };

            _patientRepo.Setup(r => r.GetPatientByCi(dto.Person.Ci)).ReturnsAsync((Patient?)null);
            _patientRepo.Setup(r => r.CreatePatient(It.IsAny<Patient>())).ReturnsAsync((Patient p) => p);

            var res = await _service.CreatePatient(dto, "creator");

            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.Equal("Paciente creado correctamente", res.Message);
        }

        [Fact]
        public async Task UpdatePatient_ShouldUpdate_WhenExists()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Name = "Old",
                LastName = "Old",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Sex = Gender.MALE,
                Ci = "ci4",
                Phone = new PhoneNumber("321")
            };

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Address = "old",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                Person = person,
                PersonId = person.Id
            };

            var dto = new Application.Dto.PatientDto
            {
                Address = "new",
                Zone = "z",
                City = "c",
                HomePhone = "321",
                Occupation = "occ",
                PlaceOccupation = "place",
                Person = new Application.Dto.PersonDto
                {
                    Name = "Old",
                    LastName = "Old",
                    BirthDate = DateTime.UtcNow.AddYears(-30).ToString("yyyy/MM/dd"),
                    Sex = "MALE",
                    Ci = "ci4",
                    Email = "e@e.com",
                    Phone = "321",
                    Profession = "prof"
                },
                State = "ACTIVE"
            };

            _patientRepo.Setup(r => r.GetPatientById(patient.Id)).ReturnsAsync(patient);
            _patientRepo.Setup(r => r.UpdatePatient(It.IsAny<Patient>())).ReturnsAsync((Patient p) => p);

            var res = await _service.UpdatePatient(patient.Id, dto, "upd");

            Assert.Equal(patient.Id, res.Id);
            Assert.Equal("Paciente actualizado correctamente", res.Message);
            Assert.Equal("new", patient.Address);
        }

        [Fact]
        public async Task DeletePatient_ShouldSoftDelete_WhenExists()
        {
            var id = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "D", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var patient = new Patient { Id = id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "occ", PlaceOccupation = "place" };
            _patientRepo.Setup(r => r.GetPatientById(id)).ReturnsAsync(patient);
            _patientRepo.Setup(r => r.UpdatePatient(It.IsAny<Patient>())).ReturnsAsync(patient);

            await _service.DeletePatient(id, "test-user");

            _patientRepo.Verify(r => r.UpdatePatient(It.Is<Patient>(p => p.State == States.INACTIVE)), Times.Once);
        }
    }
}
