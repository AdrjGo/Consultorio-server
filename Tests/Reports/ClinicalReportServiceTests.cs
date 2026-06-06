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

namespace Consultorio.Tests.Reports
{
    public class ClinicalReportServiceTests
    {
        private readonly Mock<IClinicalReportRepository> _repository = new();
        private readonly ClinicalReportService _service;

        public ClinicalReportServiceTests()
        {
            _service = new ClinicalReportService(_repository.Object);
        }

        private static DateTime DefaultCreatedAt => DateTime.UtcNow;

        [Fact]
        public async Task GetClinicalReportDataAsync_ReturnsNull_WhenPatientNotFound()
        {
            var patientId = Guid.NewGuid();
            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync((Patient?)null);

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_ReturnsNull_WhenPatientHasNoPerson()
        {
            var patientId = Guid.NewGuid();
            var patient = new Patient
            {
                Id = patientId,
                PersonId = Guid.NewGuid(),
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = null!
            };
            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync(patient);

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_ReturnsData_WhenPatientExists()
        {
            var patientId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                Name = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Sex = Gender.MALE,
                Ci = "12345678",
                Phone = new PhoneNumber("12345678"),
                Email = new EmailAddress("john@example.com"),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var patient = new Patient
            {
                Id = patientId,
                PersonId = personId,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = person
            };

            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync(patient);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 1)).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 2)).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 3)).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetAllPretreatmentExamsByPatientIdAsync(patientId)).ReturnsAsync(new List<PretreatmentExam>());
            _repository.Setup(r => r.GetAllMonitoringsByPatientIdAsync(patientId)).ReturnsAsync(new List<Monitoring>());

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.NotNull(result);
            Assert.Equal("John", result.Patient.Name);
            Assert.Equal("Doe", result.Patient.LastName);
            Assert.Equal("12345678", result.Patient.Ci);
            Assert.Equal("john@example.com", result.Patient.Email);
            Assert.Null(result.GeneralHistory);
            Assert.Null(result.TreatmentSummary);
            Assert.Null(result.ClinicHistory);
            Assert.Empty(result.PretreatmentExams);
            Assert.Empty(result.Monitoring);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_HandlesNullFormRes_Data()
        {
            var patientId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                Name = "Jane",
                LastName = "Smith",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)),
                Sex = Gender.FEMALE,
                Ci = "87654321",
                Phone = new PhoneNumber("87654321"),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var patient = new Patient
            {
                Id = patientId,
                PersonId = personId,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = person
            };

            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync(patient);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, It.IsAny<int>())).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetAllPretreatmentExamsByPatientIdAsync(patientId)).ReturnsAsync(new List<PretreatmentExam>());
            _repository.Setup(r => r.GetAllMonitoringsByPatientIdAsync(patientId)).ReturnsAsync(new List<Monitoring>());

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.NotNull(result);
            Assert.Null(result.GeneralHistory);
            Assert.Null(result.TreatmentSummary);
            Assert.Null(result.ClinicHistory);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_BuildsDto_WithPretreatmentExams()
        {
            var patientId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                Name = "Test",
                LastName = "User",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-35)),
                Sex = Gender.MALE,
                Ci = "11111111",
                Phone = new PhoneNumber("11111111"),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var patient = new Patient
            {
                Id = patientId,
                PersonId = personId,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = person
            };

            var pretreatmentExams = new List<PretreatmentExam>
            {
                new PretreatmentExam
                {
                    Id = Guid.NewGuid(),
                    PatientId = patientId,
                    Observations = "obs1",
                    Interconsultation = "int1",
                    Piece = "piece1",
                    Caries = true,
                    Treatment = "treatment1",
                    State = States.ACTIVE,
                    CreatedBy = "test",
                    CreatedAt = DefaultCreatedAt
                },
                new PretreatmentExam
                {
                    Id = Guid.NewGuid(),
                    PatientId = patientId,
                    Observations = "obs2",
                    Interconsultation = "int2",
                    Piece = "piece2",
                    Caries = false,
                    Treatment = null,
                    State = States.ACTIVE,
                    CreatedBy = "test",
                    CreatedAt = DefaultCreatedAt
                }
            };

            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync(patient);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, It.IsAny<int>())).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetAllPretreatmentExamsByPatientIdAsync(patientId)).ReturnsAsync(pretreatmentExams);
            _repository.Setup(r => r.GetAllMonitoringsByPatientIdAsync(patientId)).ReturnsAsync(new List<Monitoring>());

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.NotNull(result);
            Assert.Equal(2, result.PretreatmentExams.Count);
            Assert.Equal("obs1", result.PretreatmentExams[0].Observations);
            Assert.True(result.PretreatmentExams[0].Caries);
            Assert.Equal("treatment1", result.PretreatmentExams[0].Treatment);
            Assert.Equal("obs2", result.PretreatmentExams[1].Observations);
            Assert.False(result.PretreatmentExams[1].Caries);
            Assert.Null(result.PretreatmentExams[1].Treatment);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_BuildsDto_WithMonitorings()
        {
            var patientId = Guid.NewGuid();
            var appointmentId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                Name = "Monitor",
                LastName = "Patient",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-40)),
                Sex = Gender.FEMALE,
                Ci = "22222222",
                Phone = new PhoneNumber("22222222"),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var patient = new Patient
            {
                Id = patientId,
                PersonId = personId,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = person
            };
            var appointment = new Appointment
            {
                Id = appointmentId,
                PatientId = patientId,
                ProfessionalId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(-10).AddHours(1),
                Type = AppointmentType.Consulta,
                Status = AppointmentStatus.Confirmado,
                LifeStatus = AppointmentLifeStatus.Completada,
                Reason = "checkup",
                Observations = "Appointment observations",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var monitorings = new List<Monitoring>
            {
                new Monitoring
                {
                    Id = Guid.NewGuid(),
                    AppointmentId = appointmentId,
                    Nomenclature = "NOM001",
                    Treatment = "Treatment A",
                    Appointment = appointment,
                    EvidenceFiles = new List<EvidenceFile>(),
                    State = States.ACTIVE,
                    CreatedBy = "test",
                    CreatedAt = DefaultCreatedAt
                }
            };

            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId)).ReturnsAsync(patient);
            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, It.IsAny<int>())).ReturnsAsync((FormRes?)null);
            _repository.Setup(r => r.GetAllPretreatmentExamsByPatientIdAsync(patientId)).ReturnsAsync(new List<PretreatmentExam>());
            _repository.Setup(r => r.GetAllMonitoringsByPatientIdAsync(patientId)).ReturnsAsync(monitorings);

            var result = await _service.GetClinicalReportDataAsync(patientId);

            Assert.NotNull(result);
            Assert.Single(result.Monitoring);
            Assert.Equal("NOM001", result.Monitoring[0].Nomenclature);
            Assert.Equal("Treatment A", result.Monitoring[0].Treatment);
            Assert.Equal("Appointment observations", result.Monitoring[0].Observations);
            Assert.NotNull(result.Monitoring[0].Files);
        }

        [Fact]
        public async Task GetClinicalReportDataAsync_UsesParallelQueries()
        {
            var patientId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                Name = "Parallel",
                LastName = "Test",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Sex = Gender.MALE,
                Ci = "33333333",
                Phone = new PhoneNumber("33333333"),
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt
            };
            var patient = new Patient
            {
                Id = patientId,
                PersonId = personId,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                State = States.ACTIVE,
                CreatedBy = "test",
                CreatedAt = DefaultCreatedAt,
                Person = person
            };

            var callOrder = new List<string>();
            var tcs1 = new TaskCompletionSource<FormRes?>();
            var tcs2 = new TaskCompletionSource<List<PretreatmentExam>>();
            var tcs3 = new TaskCompletionSource<List<Monitoring>>();

            _repository.Setup(r => r.GetPatientWithPersonAsync(patientId))
                .Returns(async () => { callOrder.Add("patient"); return patient; });

            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 1))
                .Returns(async () => { callOrder.Add("form1"); return await tcs1.Task; });

            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 2))
                .Returns(async () => { callOrder.Add("form2"); return await tcs1.Task; });

            _repository.Setup(r => r.GetLatestFormResponseAsync(patientId, 3))
                .Returns(async () => { callOrder.Add("form3"); return await tcs1.Task; });

            _repository.Setup(r => r.GetAllPretreatmentExamsByPatientIdAsync(patientId))
                .Returns(async () => { callOrder.Add("pretreatment"); return await tcs2.Task; });

            _repository.Setup(r => r.GetAllMonitoringsByPatientIdAsync(patientId))
                .Returns(async () => { callOrder.Add("monitorings"); return await tcs3.Task; });

            var task = _service.GetClinicalReportDataAsync(patientId);

            tcs1.SetResult(null);
            tcs2.SetResult(new List<PretreatmentExam>());
            tcs3.SetResult(new List<Monitoring>());

            await task;

            Assert.Equal(6, callOrder.Count);
            Assert.Equal("patient", callOrder[0]);
            var parallelCalls = callOrder.Skip(1).ToList();
            Assert.Contains("form1", parallelCalls);
            Assert.Contains("form2", parallelCalls);
            Assert.Contains("form3", parallelCalls);
            Assert.Contains("pretreatment", parallelCalls);
            Assert.Contains("monitorings", parallelCalls);
        }
    }
}