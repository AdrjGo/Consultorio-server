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
                PermissionsOnRole = r.RolePermissions.Count.ToString(),
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
            var roleByname = await _roleRepository.GetRoleByName(dto.Name);
            if (roleByname != null)
                throw new KeyNotFoundException($"Ya existe un rol con nombre: {dto.Name}");

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

        public async Task<RoleMessageResponse> CreateRoleWithPermissions(RoleWithPermissionsDto dto, string creatorName)
        {
            var roleByname = await _roleRepository.GetRoleByName(dto.Role.Name);
            if (roleByname != null)
                throw new KeyNotFoundException($"Ya existe un rol con nombre: {dto.Role.Name}");

            var role = new Role
            {
                Id = Guid.CreateVersion7(),
                Name = dto.Role.Name,
                Description = dto.Role.Description ?? "",
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            var rolePermissions = dto.Permissions.Select(permissionId => new RolePermission
            {
                Id = Guid.CreateVersion7(),
                RoleId = role.Id,
                PermissionId = permissionId,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName
            }).ToList();

            await _roleRepository.CreateRoleWithPermissions(role, rolePermissions);

            return new RoleMessageResponse
            {
                Message = "Rol creado y permisos asignados correctamente",
            };
        }

        // public async Task AssignPermissionsToRole(IEnumerable<RolePermissionDto> rolePermissions, string creatorName)
        // {
        //     var rolePermission = rolePermissions.Select(rp => new RolePermission
        //     {
        //         Id = Guid.CreateVersion7(),
        //         RoleId = rp.RoleId,
        //         PermissionId = rp.PermissionId,
        //         State = States.ACTIVE,
        //         CreatedAt = DateTime.UtcNow,
        //         CreatedBy = creatorName,
        //     }).ToList();

        //     await _rolePermissionRepository.AssignPermissionToRole(rolePermission);
        // }

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
