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

namespace Consultorio.Tests.Management
{
    public class RoleRepositoryTests
    {
        private DBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DBContext(options);
        }

        [Fact]
        public async Task CreateAndGetRole_Works()
        {
            using var ctx = CreateContext();
            var repo = new RoleRespository(ctx);

            var role = new Role { Id = Guid.NewGuid(), Name = "r", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            await repo.CreateRole(role);

            var got = await repo.GetRoleById(role.Id);
            Assert.NotNull(got);
            Assert.Equal(role.Id, got.Id);
        }

        [Fact]
        public async Task GetRolesByUserId_Filters()
        {
            using var ctx = CreateContext();
            var repo = new RoleRespository(ctx);

            var r1 = new Role { Id = Guid.NewGuid(), Name = "A", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var ur = new UserRole { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), RoleId = r1.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Role = r1 };
            r1.UserRoles = new List<UserRole>{ ur };
            ctx.Roles.Add(r1);
            await ctx.SaveChangesAsync();

            var res = (await repo.GetRolesByUserId(ur.UserId)).ToList();
            Assert.Single(res);
            Assert.Equal("A", res[0].Name);
        }
    }
}
