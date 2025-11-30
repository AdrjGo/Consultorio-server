using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain.Entities;
using Domain.Enum;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Consultorio.Tests.Appointments
{
    public class AppointmentRepositoryTests
    {
        private DBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DBContext(options);
        }

        [Fact]
        public async Task CreateAndGetAppointmentById_Works()
        {
            using var ctx = CreateContext();
            var repo = new AppointmentRepository(ctx);

            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "P", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var p = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Sender = "" };
            ctx.Patients.Add(p);
            await ctx.SaveChangesAsync();

            var doctor = new User
            {
                Id = Guid.NewGuid(),
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Doc",
                    LastName = "Test",
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                    Sex = Gender.MALE,
                    Ci = "c",
                    Phone = new PhoneNumber("1")
                },
                PersonId = Guid.NewGuid(),
                State = States.ACTIVE,
                Password = "p",
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow
            };

            ctx.Users.Add(doctor);
            await ctx.SaveChangesAsync();

            var ap = new Appointment { Id = Guid.NewGuid(), PatientId = p.Id, ProfessionalId = doctor.Id, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", Observations = "o", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            await repo.CreateAppointment(ap);

            var got = await repo.GetAppointmentById(ap.Id);
            Assert.NotNull(got);
            Assert.Equal(ap.Id, got.Id);
        }

        [Fact]
        public async Task GetAppointmentsByDate_Filters()
        {
            using var ctx = CreateContext();
            var repo = new AppointmentRepository(ctx);

            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "Anna", LastName = "X", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.FEMALE, Ci = "c1", Phone = new PhoneNumber("1") };
            var p = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Sender = " " };
            ctx.Patients.Add(p);

            var doctor = new User
            {
                Id = Guid.NewGuid(),
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Doc",
                    LastName = "Test",
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                    Sex = Gender.MALE,
                    Ci = "c",
                    Phone = new PhoneNumber("1")
                },
                PersonId = Guid.NewGuid(),
                State = States.ACTIVE,
                Password = "p",
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow
            };

            ctx.Users.Add(doctor);
            await ctx.SaveChangesAsync();

            var now = DateTime.UtcNow;
            var a1 = new Appointment { Id = Guid.NewGuid(), PatientId = p.Id, ProfessionalId = doctor.Id, StartDate = now.AddDays(-1), EndDate = now.AddDays(-1).AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Observations = "Prueba" };
            var a2 = new Appointment { Id = Guid.NewGuid(), PatientId = p.Id, ProfessionalId = doctor.Id, StartDate = now.AddDays(1), EndDate = now.AddDays(1).AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Observations = "Prueba1" };

            ctx.Appointments.AddRange(a1, a2);
            await ctx.SaveChangesAsync();

            var res = (await repo.GetAppointmentsByDate(now.AddDays(-2), now)).ToList();
            Assert.Single(res);
            Assert.Contains(res, x => x.Id == a1.Id);
        }

        [Fact]
        public async Task GetAppointmentsByPatientId_Filters()
        {
            using var ctx = CreateContext();
            var repo = new AppointmentRepository(ctx);

            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "B", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.FEMALE, Ci = "c1", Phone = new PhoneNumber("1") };
            var p = new Patient { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, Address = "a", Zone = "z", City = "c", Occupation = "o", PlaceOccupation = "p", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Sender = "" };
            ctx.Patients.Add(p);

            var doctor = new User
            {
                Id = Guid.NewGuid(),
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Doc",
                    LastName = "Test",
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                    Sex = Gender.MALE,
                    Ci = "c",
                    Phone = new PhoneNumber("1")
                },
                PersonId = Guid.NewGuid(),
                State = States.ACTIVE,
                Password = "p",
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow
            };

            ctx.Users.Add(doctor);
            await ctx.SaveChangesAsync();

            var a1 = new Appointment { Id = Guid.NewGuid(), PatientId = p.Id, ProfessionalId = doctor.Id, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), Type = AppointmentType.Consulta, Status = AppointmentStatus.Programado, Reason = "r", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Observations = "Observación de prueba" };
            ctx.Appointments.Add(a1);
            await ctx.SaveChangesAsync();

            var res = (await repo.GetAppointmentsByPatientId(p.Id)).ToList();
            Assert.Single(res);
            Assert.Equal(a1.Id, res[0].Id);
        }
    }
}
