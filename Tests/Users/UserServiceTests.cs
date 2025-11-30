using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Consultorio.Tests.Users
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepo = new();
        private readonly UserService _service;

        public UserServiceTests()
        {
            _service = new UserService(_userRepo.Object);
        }

        [Fact]
        public async Task GetUserById_ReturnsResponse_WhenExists()
        {
            var role = new Role { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "Admin", Description = "d" };
            var userRole = new UserRole { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), RoleId = role.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Role = role };

            var person = new Person
            {
                Id = Guid.NewGuid(),
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Name = "U",
                LastName = "L",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                Sex = Gender.MALE,
                Ci = "ci",
                Email = new EmailAddress("a@b.com"),
                Phone = new PhoneNumber("123")
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                PersonId = person.Id,
                State = States.ACTIVE,
                CreatedBy = "t",
                CreatedAt = DateTime.UtcNow,
                Person = person,
                UserRoles = new List<UserRole> { userRole }
            };

            userRole.UserId = user.Id;

            _userRepo.Setup(r => r.GetUserById(user.Id)).ReturnsAsync(user);

            var res = await _service.GetUserById(user.Id);

            Assert.Equal(user.Id, res.Id);
            Assert.Equal("U", res.Person.Name);
            Assert.Single(res.Roles);
        }

        [Fact]
        public async Task GetUserById_Throws_WhenNotFound()
        {
            _userRepo.Setup(r => r.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetUserById(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetUsers_ReturnsMappedList()
        {
            var role = new Role { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "Role1", Description = "d" };
            var userRole = new UserRole { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), RoleId = role.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Role = role };

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    PersonId = Guid.NewGuid(),
                    State = States.ACTIVE,
                    CreatedBy = "t",
                    CreatedAt = DateTime.UtcNow,
                    Person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "A", LastName = "B", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)), Sex = Gender.FEMALE, Ci = "1", Email = new EmailAddress("e@e.com"), Phone = new PhoneNumber("1") },
                    UserRoles = new List<UserRole>{ userRole },
                }
            };

            users[0].UserRoles[0].UserId = users[0].Id;

            _userRepo.Setup(r => r.GetUsers(null, null, null)).ReturnsAsync(users);

            var res = (await _service.GetUsers()).ToList();

            Assert.Single(res);
            Assert.Equal(users[0].Id, res[0].Id);
        }

        [Fact]
        public async Task GetUserByName_ThrowsOnNullName()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetUserByName(null!));
        }

        [Fact]
        public async Task GetUserByName_Throws_WhenNotFound()
        {
            _userRepo.Setup(r => r.GetUserByName(It.IsAny<string>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetUserByName("nope"));
        }

        [Fact]
        public async Task GetUserByName_ReturnsMapped()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "N", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)), Sex = Gender.MALE, Ci = "ci", Phone = new PhoneNumber("1") };
            var user = new User { Id = Guid.NewGuid(), PersonId = person.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Person = person };
            _userRepo.Setup(r => r.GetUserByName("N")).ReturnsAsync(user);
            var res = await _service.GetUserByName("N");
            Assert.Equal(user.Id, res.Id);
            Assert.Equal(user.Person.Name, res.Person.Name);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAll()
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), PersonId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "X", LastName = "Y", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.MALE, Ci = "c", Email = new EmailAddress("a@b.com"), Phone = new PhoneNumber("2") }, UserRoles = new List<UserRole>(), }
            };
            _userRepo.Setup(r => r.GetAllUsers()).ReturnsAsync(users);
            var res = (await _service.GetAllUsers()).ToList();
            Assert.Single(res);
            Assert.Equal(users[0].Id, res[0].Id);
        }

        [Fact]
        public async Task CreateUser_CreatesAndReturnsMessage()
        {
            var dto = new UserDto
            {
                Password = "pwd",
                Person = new Application.Dto.PersonDto { Name = "C", LastName = "D", 
                    BirthDate = DateTime.UtcNow.AddYears(-25).ToString("yyyy/MM/dd"), Sex = "MALE", Ci = "ci", Email = "a@b.com",
                    Phone = "1" }
            };

            _userRepo.Setup(r => r.CreateUser(It.IsAny<User>())).ReturnsAsync((User u) => u);

            var res = await _service.CreateUser(dto, "creator");

            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.Equal("Usuario creado correctamente", res.Message);
        }

        [Fact]
        public async Task UpdateUser_Throws_WhenNotFound()
        {
            _userRepo.Setup(r => r.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateUser(Guid.NewGuid(), new UserWithOutPasswordDto { Person = new Application.Dto.PersonDto { Name = "a", LastName = "b", BirthDate = DateTime.UtcNow.AddYears(-20).ToString("dd/MM/yyyy"), Sex = "MALE", Ci = "c", Email = "e@e.com", Phone = "1" } }, "up"));
        }

        [Fact]
        public async Task UpdateUser_Updates_WhenExists()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "Old", LastName = "L", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.MALE, Ci = "c", Email = new EmailAddress("a@b.com"), Phone = new PhoneNumber("1") };
            var user = new User { Id = Guid.NewGuid(), PersonId = person.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Person = person };
            _userRepo.Setup(r => r.GetUserById(user.Id)).ReturnsAsync(user);
            _userRepo.Setup(r => r.UpdateUser(It.IsAny<User>())).ReturnsAsync((User u) => u);

            var dto = new UserWithOutPasswordDto { Person = new Application.Dto.PersonDto { Name = "New", LastName = "L", BirthDate = DateTime.UtcNow.AddYears(-40).ToString("yyyy/MM/dd"), Sex = "MALE", Ci = "c", Email = "e@e.com", Phone = "1" } };

            var res = await _service.UpdateUser(user.Id, dto, "up");

            Assert.Equal(user.Id, res.Id);
            Assert.Equal("Usuario actualizado correctamente", res.Message);
            Assert.Equal("New", user.Person.Name);
        }

        [Fact]
        public async Task ChangeState_Throws_WhenNotFound()
        {
            _userRepo.Setup(r => r.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.ChangeState(Guid.NewGuid(), new UserChangeStateDto { State = "INACTIVE" }, "u"));
        }

        [Fact]
        public async Task ChangeState_Updates_WhenExists()
        {
            var person = new Person { Id = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Name = "x", LastName = "y", BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)), Sex = Gender.MALE, Ci = "c", Phone = new PhoneNumber("1") };
            var user = new User { Id = Guid.NewGuid(), PersonId = person.Id, State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow, Person = person };
            _userRepo.Setup(r => r.GetUserById(user.Id)).ReturnsAsync(user);
            _userRepo.Setup(r => r.UpdateUser(It.IsAny<User>())).ReturnsAsync((User u) => u);

            var res = await _service.ChangeState(user.Id, new UserChangeStateDto { State = "INACTIVE" }, "u");

            Assert.Equal(user.Id, res.Id);
            Assert.Equal("Estado actualizado correctamente", res.Message);
        }

        [Fact]
        public async Task DeleteUser_Throws_WhenNotFound()
        {
            _userRepo.Setup(r => r.GetUserById(It.IsAny<Guid>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteUser(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteUser_Deletes_WhenExists()
        {
            var id = Guid.NewGuid();
            var user = new User { Id = id, PersonId = Guid.NewGuid(), State = States.ACTIVE, CreatedBy = "t", CreatedAt = DateTime.UtcNow };
            _userRepo.Setup(r => r.GetUserById(id)).ReturnsAsync(user);
            _userRepo.Setup(r => r.DeleteUser(id)).ReturnsAsync(user);

            await _service.DeleteUser(id);

            _userRepo.Verify(r => r.DeleteUser(id), Times.Once);
        }
    }
}
