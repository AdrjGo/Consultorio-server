using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Consultorio.Tests.Appointments
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _repo = new();
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _service = new AppointmentService(_repo.Object);
        }

        [Fact]
        public async Task GetAllAppointments_MapsCorrectly()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "P", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var patient = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", Observations = "o", Patient = patient, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAllAppointments()).ReturnsAsync(new List<Appointment> { ap });

            var res = (await _service.GetAllAppointments()).ToList();

            Assert.Single(res);
            Assert.Equal(ap.Id, res[0].Id);
            Assert.Equal(patient.Id, res[0].PatientId);
            Assert.Equal(patient.Person.Name, res[0].Patient.PatientPerson.Name);
        }

        [Fact]
        public async Task GetAppointmentById_ReturnsMapped_WhenExists()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "P2", LastName = "L2", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.FEMALE, Ci = "c2", Phone = new PhoneNumber("2") };
            var patient = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", Observations = "o", Patient = patient, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAppointmentById(ap.Id)).ReturnsAsync(ap);

            var res = await _service.GetAppointmentById(ap.Id);

            Assert.Equal(ap.Id, res.Id);
            Assert.Equal(patient.Person.Name, res.Patient.PatientPerson.Name);
        }

        [Fact]
        public async Task GetAppointmentById_Throws_WhenNotFound()
        {
            _repo.Setup(r => r.GetAppointmentById(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAppointmentById(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetAppointmentsByDate_ReturnsList()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "PD", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var patient = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", Observations = "o", Patient = patient, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAppointmentsByDate(It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).ReturnsAsync(new List<Appointment> { ap });

            var res = (await _service.GetAppointmentsByDate(DateTime.UtcNow.ToString(), DateTime.UtcNow.AddDays(1).ToString())).ToList();
            Assert.Single(res);
            Assert.Equal(ap.Id, res[0].Id);
        }

        [Fact]
        public async Task GetAppointmentsByPatientId_ReturnsList_WhenExists()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "PX", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var patient = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", Observations = "o", Patient = patient, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAppointmentsByPatientId(patient.Id)).ReturnsAsync(new List<Appointment> { ap });

            var res = (await _service.GetAppointmentsByPatientId(patient.Id)).ToList();
            Assert.Single(res);
            Assert.Equal(ap.Id, res[0].Id);
        }

        [Fact]
        public async Task GetAppointmentsByPatientId_Throws_WhenNotFound()
        {
            _repo.Setup(r => r.GetAppointmentsByPatientId(It.IsAny<Guid>())).ReturnsAsync((IEnumerable<Appointment>)null!);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAppointmentsByPatientId(Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateAppointment_Creates()
        {
            var dto = new Application.Dto.AppointmentDto { PatientId = Guid.NewGuid(), ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r" };
            _repo.Setup(r => r.CreateAppointment(It.IsAny<Appointment>())).ReturnsAsync((Appointment a) => a);

            var res = await _service.CreateAppointment(dto, "c");
            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.Equal("Cita creada correctamente", res.Message);
        }

        [Fact]
        public async Task UpdateAppointment_Updates_WhenExists()
        {
            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = Guid.NewGuid(), ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAppointmentById(ap.Id)).ReturnsAsync(ap);
            _repo.Setup(r => r.UpdateAppointment(It.IsAny<Appointment>())).ReturnsAsync((Appointment a) => a);

            var dto = new Application.Dto.AppointmentUpdateDto { StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(1).AddHours(1), Reason = "new" };

            var res = await _service.UpdateAppointment(ap.Id, dto, "u");
            Assert.Equal(ap.Id, res.Id);
            Assert.Equal("Cita editada correctamente", res.Message);
            Assert.Equal(dto.Reason, ap.Reason);
        }

        [Fact]
        public async Task UpdateAppointment_Throws_WhenNotFound()
        {
            _repo.Setup(r => r.GetAppointmentById(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAppointment(Guid.NewGuid(), new Application.Dto.AppointmentUpdateDto { Reason = "x" }, "u"));
        }

        [Fact]
        public async Task DeleteAppointment_Deletes_WhenExists()
        {
            var id = Guid.NewGuid();
            var ap = new Appointment { Id = id, PatientId = Guid.NewGuid(), ProfessionalId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _repo.Setup(r => r.GetAppointmentById(id)).ReturnsAsync(ap);
            _repo.Setup(r => r.DeleteAppointment(id)).Returns(Task.CompletedTask);

            await _service.DeleteAppointment(id);

            _repo.Verify(r => r.DeleteAppointment(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAppointment_Throws_WhenNotFound()
        {
            _repo.Setup(r => r.GetAppointmentById(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAppointment(Guid.NewGuid()));
        }
    }
}
