using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IEnumerable<RoleResponse>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            if (roles == null)
                throw new KeyNotFoundException($"No se encontró ningún rol");

            return roles.Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                UsersUsingRole = r.UserRoles.Count.ToString(),
            });
        }

        public async Task<RoleResponse> GetRoleByName(string name)
        {
            var role = await _roleRepository.GetRoleByName(name);

            if (role == null)
                throw new KeyNotFoundException($"No se encontró el rol con nombre: {name}");

            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesByUserId(Guid userId)
        {
            var roles = await _roleRepository.GetRolesByUserId(userId);
            if (roles == null)
                throw new KeyNotFoundException($"No se encontró ningún rol");

            return roles.Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
            });
        }

        public async Task<RoleResponse> CreateRole(RoleDto dto, string creatorName)
        {
            var role = new Role
            {
                Id = Guid.CreateVersion7(),
                Name = dto.Name,
                Description = dto.Description ?? "",
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            await _roleRepository.CreateRole(role);

            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
        }

        public async Task<RoleResponse> UpdateRole(Guid id, RoleDto dto, string creatorName)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
                throw new KeyNotFoundException($"No se encontró el rol con id {id}");

            role.Name = dto.Name;
            role.Description = dto.Description ?? "";
            role.UpdatedAt = DateTime.UtcNow;
            role.UpdatedBy = creatorName;

            await _roleRepository.UpdateRole(role);

            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
        }

        public async Task DeleteRole(Guid id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
                throw new KeyNotFoundException($"No se encontró el rol con id {id}");

            await _roleRepository.DeleteRole(id);
        }
    }
}
