using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> GetUser(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El id no puede estar vacío.");

            var user = await _userRepository.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException($"No se encontró ninguna persona con el id {id}");

            var personResponse = user.Person != null ? new PersonResponse
            {
                Name = user.Person.Name,
                LastName = user.Person.LastName,
                BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                Sex = user.Person.Sex.ToString(),
                Ci = user.Person.Ci,
                Email = user.Person.Email?.Value,
                Phone = user.Person.Phone?.Value,
            } : null;

            return new UserResponse
            {
                Id = user.Id,
                Person = personResponse
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Person = new PersonResponse
                {
                    Name = u.Person.Name,
                    LastName = u.Person.LastName,
                    BirthDate = u.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = u.Person.Sex.ToString(),
                    Ci = u.Person.Ci,
                    Email = u.Person.Email.Value,
                    Phone = u.Person.Phone.Value,
                }
            });
        }

        public async Task<UserResponse> CreateUser(UserDto dto)
        {
            var person = new Person
            {
                Id = Guid.CreateVersion7(),
                State = States.ACTIVE,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                Name = dto.Person.Name,
                LastName = dto.Person.LastName,
                BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Person.BirthDate), DateTimeKind.Utc),
                Sex = Enum.Parse<Gender>(dto.Person.Sex),
                Ci = dto.Person.Ci,
                Email = new EmailAddress(dto.Person.Email),
                Phone = new PhoneNumber(dto.Person.Phone),
            };

            var user = new User
            {
                Id = Guid.CreateVersion7(),
                PersonId = person.Id,
                State = States.ACTIVE,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Person = person,
                UserRoles = new List<UserRole>(),
                Appointments = new List<Appointment>()
            };

            await _userRepository.CreateUser(user);


            return new UserResponse
            {
                Id = user.Id,
                Person = new PersonResponse
                {
                    Name = user.Person.Name,
                    LastName = user.Person.LastName,
                    BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = user.Person.Sex.ToString(),
                    Ci = user.Person.Ci,
                    Email = user.Person.Email.Value,
                    Phone = user.Person.Phone.Value,
                }
            };
        }

        public async Task<User> UpdateUser(User user)
        {
            var existing = await _userRepository.GetUserById(user.Id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {user.Id}");

            return await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {id}");

            await _userRepository.DeleteUser(id);
        }
    }
}