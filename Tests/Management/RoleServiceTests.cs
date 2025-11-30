using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Consultorio.Tests.Management
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> _roleRepo = new();
        private readonly RoleService _service;

        public RoleServiceTests()
        {
            _service = new RoleService(_roleRepo.Object);
        }

        [Fact]
        public async Task GetAllRoles_ReturnsMapped_WhenExists()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "R1", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, UserRoles = new List<UserRole>{ new UserRole { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), RoleId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow } }, RolePermissions = new List<RolePermission>{ new RolePermission { Id = Guid.NewGuid(), RoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow } } };
            _roleRepo.Setup(r => r.GetAllRoles()).ReturnsAsync(new List<Role> { role });

            var res = (await _service.GetAllRoles()).ToList();

            Assert.Single(res);
            Assert.Equal(role.Id, res[0].Id);
            Assert.Equal("1", res[0].UsersUsingRole);
            Assert.Equal("1", res[0].PermissionsOnRole);
        }

        [Fact]
        public async Task GetAllRoles_Throws_WhenNone()
        {
            _roleRepo.Setup(r => r.GetAllRoles()).ReturnsAsync((IEnumerable<Role>)null!);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAllRoles());
        }

        [Fact]
        public async Task GetRoleByName_ReturnsMapped_WhenExists()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "FindMe", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _roleRepo.Setup(r => r.GetRoleByName("FindMe")).ReturnsAsync(role);

            var res = await _service.GetRoleByName("FindMe");
            Assert.Equal(role.Id, res.Id);
            Assert.Equal(role.Name, res.Name);
        }

        [Fact]
        public async Task GetRoleByName_Throws_WhenNotFound()
        {
            _roleRepo.Setup(r => r.GetRoleByName(It.IsAny<string>())).ReturnsAsync((Role?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetRoleByName("nope"));
        }

        [Fact]
        public async Task GetRolesByUserId_ReturnsMapped()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "R", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _roleRepo.Setup(r => r.GetRolesByUserId(It.IsAny<Guid>())).ReturnsAsync(new List<Role> { role });

            var res = (await _service.GetRolesByUserId(Guid.NewGuid())).ToList();
            Assert.Single(res);
            Assert.Equal(role.Id, res[0].Id);
        }

        [Fact]
        public async Task GetRoleWithPermissionsById_ReturnsMapped()
        {
            var rp = new RolePermission { Id = Guid.NewGuid(), RoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            var role = new Role { Id = Guid.NewGuid(), Name = "R", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, RolePermissions = new List<RolePermission>{ rp } };
            _roleRepo.Setup(r => r.GetRoleWithPermissionsById(role.Id)).ReturnsAsync(role);

            var res = await _service.GetRoleWithPermissionsById(role.Id);
            Assert.Equal(role.Id, res.Id);
            Assert.Single(res.Permissions);
            Assert.Equal(rp.PermissionId, res.Permissions.First());
        }

        [Fact]
        public async Task GetRoleWithPermissionsById_Throws_WhenNotFound()
        {
            _roleRepo.Setup(r => r.GetRoleWithPermissionsById(It.IsAny<Guid>())).ReturnsAsync((Role?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetRoleWithPermissionsById(Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateRole_CreatesAndReturns()
        {
            var dto = new Application.Dto.RoleDto { Name = "NewRole", Description = "desc" };
            _roleRepo.Setup(r => r.GetRoleByName(dto.Name)).ReturnsAsync((Role?)null);
            _roleRepo.Setup(r => r.CreateRole(It.IsAny<Role>())).ReturnsAsync((Role r) => r);

            var res = await _service.CreateRole(dto, "creator");
            Assert.Equal(dto.Name, res.Name);
        }

        [Fact]
        public async Task CreateRole_Throws_WhenExists()
        {
            _roleRepo.Setup(r => r.GetRoleByName(It.IsAny<string>())).ReturnsAsync(new Role { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "x", Description = "d" });
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.CreateRole(new Application.Dto.RoleDto { Name = "x" }, "c"));
        }

        [Fact]
        public async Task CreateRoleWithPermissions_Creates()
        {
            var dto = new Application.Dto.RoleWithPermissionsDto { Role = new Application.Dto.RoleDto { Name = "R", Description = "d" }, Permissions = new List<Guid> { Guid.NewGuid() } };
            _roleRepo.Setup(r => r.GetRoleByName(dto.Role.Name)).ReturnsAsync((Role?)null);
            _roleRepo.Setup(r => r.CreateRoleWithPermissions(It.IsAny<Role>(), It.IsAny<IEnumerable<RolePermission>>())).ReturnsAsync((Role r, IEnumerable<RolePermission> rp) => r);

            var res = await _service.CreateRoleWithPermissions(dto, "c");
            Assert.Equal("Rol creado y permisos asignados correctamente", res.Message);
        }

        [Fact]
        public async Task UpdateRole_Updates()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "Old", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _roleRepo.Setup(r => r.GetRoleById(role.Id)).ReturnsAsync(role);
            _roleRepo.Setup(r => r.UpdateRole(It.IsAny<Role>())).ReturnsAsync((Role r) => r);

            var res = await _service.UpdateRole(role.Id, new Application.Dto.RoleDto { Name = "New", Description = "nd" }, "u");
            Assert.Equal("New", res.Name);
        }

        [Fact]
        public async Task UpdateRole_Throws_WhenNotFound()
        {
            _roleRepo.Setup(r => r.GetRoleById(It.IsAny<Guid>())).ReturnsAsync((Role?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateRole(Guid.NewGuid(), new Application.Dto.RoleDto { Name = "x" }, "u"));
        }

        [Fact]
        public async Task UpdateRoleWithPermissions_Updates()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "R", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _roleRepo.Setup(r => r.GetRoleById(role.Id)).ReturnsAsync(role);
            _roleRepo.Setup(r => r.UpdateRoleWithPermissions(It.IsAny<Role>(), It.IsAny<IEnumerable<RolePermission>>())).ReturnsAsync((Role r, IEnumerable<RolePermission> rp) => r);

            var dto = new Application.Dto.RoleWithPermissionsDto { Role = new Application.Dto.RoleDto { Name = "R", Description = "d" }, Permissions = new List<Guid> { Guid.NewGuid() } };
            var res = await _service.UpdateRoleWithPermissions(role.Id, dto, "u");
            Assert.Equal("Rol actualizado y permisos asignados correctamente", res.Message);
        }

        [Fact]
        public async Task DeleteRole_Deletes()
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "R", Description = "d", State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _roleRepo.Setup(r => r.GetRoleById(role.Id)).ReturnsAsync(role);
            _roleRepo.Setup(r => r.DeleteRole(role.Id)).Returns(Task.CompletedTask);

            await _service.DeleteRole(role.Id);

            _roleRepo.Verify(r => r.DeleteRole(role.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteRole_Throws_WhenNotFound()
        {
            _roleRepo.Setup(r => r.GetRoleById(It.IsAny<Guid>())).ReturnsAsync((Role?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteRole(Guid.NewGuid()));
        }
    }
}
