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

        public async Task<UserResponse> GetUserByName(string name)
        {
            if (name == null)
                throw new ArgumentException("El nombre no puede estar vacío.");

            var user = await _userRepository.GetUserByName(name);

            if (user == null)
                throw new KeyNotFoundException($"No se encontró al usuario: {name}");

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
                    Profession = u.Person.Profession,
                }
            });
        }

        public async Task<UserResponse> CreateUser(UserDto dto, string creatorName)
        {
            var person = new Person
            {
                Id = Guid.CreateVersion7(),
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = DateTime.UtcNow,
                Name = dto.Person.Name,
                LastName = dto.Person.LastName,
                BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Person.BirthDate), DateTimeKind.Utc),
                Sex = Enum.Parse<Gender>(dto.Person.Sex),
                Ci = dto.Person.Ci,
                Email = new EmailAddress(dto.Person.Email),
                Phone = new PhoneNumber(dto.Person.Phone),
                Profession = dto.Person.Professional,
            };

            var user = new User
            {
                Id = Guid.CreateVersion7(),
                PersonId = person.Id,
                State = States.ACTIVE,
                CreatedBy = creatorName,
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
                State = user.State,
                Person = new PersonResponse
                {
                    Name = user.Person.Name,
                    LastName = user.Person.LastName,
                    BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = user.Person.Sex.ToString(),
                    Ci = user.Person.Ci,
                    Email = user.Person.Email.Value,
                    Phone = user.Person.Phone.Value,
                    Profession = user.Person.Profession,
                }
            };
        }

        public async Task<UserResponse> UpdateUser(Guid Id, PersonDto dto, string creatorName)
        {
            var user = await _userRepository.GetUserById(Id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {Id}");

            user.Person.Name = dto.Name;
            user.Person.LastName = dto.LastName;
            user.Person.BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.BirthDate), DateTimeKind.Utc);
            user.Person.Sex = Enum.Parse<Gender>(dto.Sex);
            user.Person.Ci = dto.Ci;
            user.Person.Email = new EmailAddress(dto.Email);
            user.Person.Phone = new PhoneNumber(dto.Phone);
            user.Person.Profession = dto.Professional;

            user.Person.UpdatedAt = DateTime.UtcNow;
            user.Person.UpdatedBy = creatorName;

            await _userRepository.UpdateUser(user);

            return new UserResponse
            {
                Id = user.Id,
                State = user.State,
                Person = new PersonResponse
                {
                    Name = user.Person.Name,
                    LastName = user.Person.LastName,
                    BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = user.Person.Sex.ToString(),
                    Ci = user.Person.Ci,
                    Email = user.Person.Email.Value,
                    Phone = user.Person.Phone.Value,
                    Profession = user.Person.Profession,
                }
            };
        }

        public async Task<UserResponse> ChangeState(Guid id, UserChangeStateDto dto, string creatorName)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {id}");

            user.State = dto.State;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = creatorName;

            await _userRepository.UpdateUser(user);
            return new UserResponse
            {
                Id = user.Id,
                State = user.State,
                Person = new PersonResponse
                {
                    Name = user.Person.Name,
                    LastName = user.Person.LastName,
                    BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = user.Person.Sex.ToString(),
                    Ci = user.Person.Ci,
                    Email = user.Person.Email.Value,
                    Phone = user.Person.Phone.Value,
                    Profession = user.Person.Profession,
                }
            };
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