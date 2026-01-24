using Application.Dto;
using Application.Responses;
using Application.Utils;
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

        public async Task<UserResponse> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró al usuario: {id}");
            return new UserResponse
            {
                Id = user.Id,
                Person = new PersonResponse
                {
                    Id = user.Person.Id,
                    Name = user.Person.Name,
                    LastName = user.Person.LastName,
                    BirthDate = user.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = user.Person.Sex.ToString(),
                    Ci = user.Person.Ci,
                    Email = user.Person.Email?.Value,
                    Phone = user.Person.Phone?.Value,
                    Profession = user.Person.Profession,
                },
                Roles = user.UserRoles
                        .Select(ur => new RoleResponse
                        {
                            Id = ur.Role.Id,
                            Name = ur.Role.Name,
                            Description = ur.Role.Description,
                        }).ToList(),
                State = user.State.ToString(),
                CreatedAt = user.CreatedAt.ToString(),
                UpdatedAt = user.UpdatedAt?.ToString(),
                CreatedBy = user.CreatedBy,
                UpdatedBy = user.UpdatedBy,
            };
        }

        public async Task<IEnumerable<UserResponse>> GetUsers(string? search = null, string? state = null, string? role = null)
        {
            var users = await _userRepository.GetUsers(search, state, role);
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Person = new PersonResponse
                {
                    Id = u.Person.Id,
                    Name = u.Person.Name,
                    LastName = u.Person.LastName,
                    BirthDate = u.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = u.Person.Sex.ToString(),
                    Ci = u.Person.Ci,
                    Email = u.Person.Email?.Value,
                    Phone = u.Person.Phone?.Value,
                    Profession = u.Person.Profession,
                },
                Roles = u.UserRoles
                    .Select(ur => new RoleResponse
                    {
                        Id = ur.Role.Id,
                        Name = ur.Role.Name,
                        Description = ur.Role.Description,
                    })
                    .ToList(),
                State = u.State.ToString(),
                CreatedAt = u.CreatedAt.ToString(),
                UpdatedAt = u.UpdatedAt?.ToString(),
                CreatedBy = u.CreatedBy,
                UpdatedBy = u.UpdatedBy,
            });
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
                Id = user.Person.Id,
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
                    Id = u.Person.Id,
                    Name = u.Person.Name,
                    LastName = u.Person.LastName,
                    BirthDate = u.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = u.Person.Sex.ToString(),
                    Ci = u.Person.Ci,
                    Email = u.Person.Email.Value,
                    Phone = u.Person.Phone.Value,
                    Profession = u.Person.Profession,
                },
                Roles = u.UserRoles
                    .Select(ur => new RoleResponse
                    {
                        Id = ur.Role.Id,
                        Name = ur.Role.Name,
                        Description = ur.Role.Description,

                    })
                    .ToList(),
                State = u.State.ToString(),
            });
        }

        public async Task<UserMessageResponse> CreateUser(UserDto dto, string creatorName)
        {
            var person = new Person
            {
                Id = Guid.CreateVersion7(),
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                Name = dto.Person.Name,
                LastName = dto.Person.LastName,
                BirthDate = DateOnly.Parse(dto.Person.BirthDate),
                Sex = Enum.Parse<Gender>(dto.Person.Sex),
                Ci = dto.Person.Ci,
                Email = new EmailAddress(dto.Person.Email),
                Phone = new PhoneNumber(dto.Person.Phone),
                Profession = dto.Person.Profession,
            };

            var user = new User
            {
                Id = Guid.CreateVersion7(),
                PersonId = person.Id,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Person = person,
                UserRoles = new List<UserRole>(),
                Appointments = new List<Appointment>()
            };

            await _userRepository.CreateUser(user);

            return new UserMessageResponse
            {
                Id = user.Id,
                Message = "Usuario creado correctamente"
            };
        }

        public async Task<UserMessageResponse> UpdateUser(Guid Id, UserWithOutPasswordDto dto, string creatorName)
        {
            var user = await _userRepository.GetUserById(Id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {Id}");

            user.Person.Name = dto.Person.Name;
            user.Person.LastName = dto.Person.LastName;
            user.Person.BirthDate = DateOnly.Parse(dto.Person.BirthDate);
            user.Person.Sex = Enum.Parse<Gender>(dto.Person.Sex);
            user.Person.Ci = dto.Person.Ci;
            user.Person.Email = new EmailAddress(dto.Person.Email);
            user.Person.Phone = new PhoneNumber(dto.Person.Phone);
            user.Person.Profession = dto.Person.Profession;

            user.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            user.UpdatedBy = creatorName;

            await _userRepository.UpdateUser(user);

            return new UserMessageResponse
            {
                Id = user.Id,
                Message = "Usuario actualizado correctamente"
            };
        }

        public async Task<UserMessageResponse> ChangeState(Guid id, UserChangeStateDto dto, string creatorName)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona con id {id}");

            user.State = Enum.Parse<States>(dto.State);
            user.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            user.UpdatedBy = creatorName;

            await _userRepository.UpdateUser(user);
            return new UserMessageResponse
            {
                Id = user.Id,
                Message = "Estado actualizado correctamente"
            };
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"No se encontró la persona");

            await _userRepository.DeleteUser(id);
        }
    }
}