using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain.Entities;
using Domain.Enum;
using Domain.ValueObjects;
using System.Linq;

namespace Consultorio.Tests.Patients
{
    public class PatientRepositoryTests
    {
        private DBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DBContext(options);
        }

        [Fact]
        public async Task CreateAndGetPatientById_Works()
        {
            using var ctx = CreateContext();
            var repo = new PatientRespository(ctx);

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = "Repo",
                LastName = "Test",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
                Sex = Gender.MALE,
                Ci = "r1",
                Phone = new PhoneNumber("1"),
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test", // <-- FIX: miembro requerido
            };

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Person = person,
                PersonId = person.Id,
                State = States.ACTIVE,
                Address = "addr",
                Zone = "z",
                City = "c",
                Occupation = "occ",
                PlaceOccupation = "place",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test", // <-- FIX: miembro requerido
                Sender = "",
            };

            await repo.CreatePatient(patient);

            var got = await repo.GetPatientById(patient.Id);
            Assert.NotNull(got);
            Assert.Equal(patient.Id, got.Id);
        }

        [Fact]
        public async Task GetPatientsByName_Filters()
        {
            using var ctx = CreateContext();
            var repo = new PatientRespository(ctx);

            var p1 = new Patient
            {
                Id = Guid.NewGuid(),
                Address = "addr1",
                Zone = "z1",
                City = "c1",Occupation = "occ1",
                PlaceOccupation = "place1",
                Sender ="",
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Anna",
                    LastName = "X",
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                    Sex = Gender.FEMALE,
                    Ci = "c1",
                    Phone = new PhoneNumber("1"),
                    State = States.ACTIVE,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "test",
                },
                PersonId = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test",
            };
            var p2 = new Patient
            {
                Id = Guid.NewGuid(),
                Address = "addr2",
                Zone = "z2",
                City = "c2",
                Occupation = "occ2",
                PlaceOccupation = "place2",
                Sender = "",
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Bob",
                    LastName = "Y",
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                    Sex = Gender.MALE,
                    Ci = "c2",
                    Phone = new PhoneNumber("2"),
                    State = States.ACTIVE,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "test",
                },
                PersonId = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test",
            };

            ctx.Patients.AddRange(p1, p2);
            await ctx.SaveChangesAsync();

            var res = (await repo.GetPatientsByName("Anna")).ToList();
            Assert.Single(res);
            Assert.Equal("Anna", res[0].Person.Name);
        }

        [Fact]
        public async Task GetPatientsPagedAsync_Paginates()
        {
            using var ctx = CreateContext();
            var repo = new PatientRespository(ctx);

            for (int i = 0; i < 5; i++)
            {
                var p = new Patient
                {
                    Id = Guid.NewGuid(),
                    Address = "addr3",
                    Zone = "z3",
                    City = "c3",
                    Occupation = "occ3",
                    PlaceOccupation = "place3",
                    Sender = "",
                    Person = new Person
                    {
                        Id = Guid.NewGuid(),
                        Name = "N" + i,
                        LastName = "L",
                        BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
                        Sex = Gender.MALE,
                        Ci = "c" + i,
                        Phone = new PhoneNumber("1"),
                        State = States.ACTIVE,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "test",
                    },
                    PersonId = Guid.NewGuid(),
                    State = States.ACTIVE,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "test",
                };
                ctx.Patients.Add(p);
            }
            await ctx.SaveChangesAsync();

            var (items, total) = await repo.GetPatientsPagedAsync(2, 2, null, null);
            Assert.Equal(5, total);
            Assert.Equal(2, items.Count());
        }
    }
}
