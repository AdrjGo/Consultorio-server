using Domain.Entities;
using Domain.Enum;
using Domain.ValueObjects;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Consultorio.Tests.Users
{
    public class UserRepositoryTests
    {
        private DBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DBContext(options);
        }

        [Fact]
        public async Task CreateAndGetUserById_Works()
        {
            using var ctx = CreateContext();
            var repo = new UserRespository(ctx);

            var person = new Person { Id = Guid.NewGuid(), Name = "UR", LastName = "T", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)), Sex = Gender.MALE, Ci = "u1", Phone = new PhoneNumber("1"), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var user = new User { Id = Guid.NewGuid(), Person = person, PersonId = person.Id, State = States.ACTIVE, Password = "p", CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            await repo.CreateUser(user);

            var got = await repo.GetUserById(user.Id);
            Assert.NotNull(got);
            Assert.Equal(user.Id, got.Id);
        }

        //[Fact]
        //public async Task GetUsers_FiltersBySearch()
        //{
        //    using var ctx = CreateContext();
        //    var repo = new UserRespository(ctx);

        //    var u1 = new User { Id = Guid.NewGuid(), Person = new Person { Id = Guid.NewGuid(), Name = "Anna", LastName = "X", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.FEMALE, Ci = "c1", Phone = new PhoneNumber("1"), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow }, PersonId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Password = "Prueba1234!" };
        //    var u2 = new User { Id = Guid.NewGuid(), Person = new Person { Id = Guid.NewGuid(), Name = "Bob", LastName = "Y", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.MALE, Ci = "c2", Phone = new PhoneNumber("2"), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow }, PersonId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Password = "Prueba5678!" };

        //    ctx.Users.AddRange(u1, u2);
        //    await ctx.SaveChangesAsync();

        //    var res = (await repo.GetUsers("Anna", null, null)).ToList();
        //    Assert.Single(res);
        //    Assert.Equal("Anna", res[0].Person.Name);
        //}
    }
}
