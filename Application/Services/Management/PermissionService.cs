using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class PermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionResponse>> GetAllPermissions()
        {
            var permissions = await _permissionRepository.GetAllPermissions();
            if (permissions == null)
                throw new KeyNotFoundException($"No se encontró ningún permiso");

            return permissions.Select(p => new PermissionResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            });
        }

        public async Task<PermissionResponse> GetPermissionByName(string name)
        {
            var permission = await _permissionRepository.GetPermissionByName(name);

            if (permission == null)
                throw new KeyNotFoundException($"No se encontró el permiso con nombre: {name}");

            return new PermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
            };
        }

        public async Task<PermissionResponse> CreatePermission(PermissionDto dto, string creatorName)
        {
            var permission = new Permission
            {
                Id = Guid.CreateVersion7(),
                Name = dto.Name,
                Description = dto.Description ?? "",
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            await _permissionRepository.CreatePermission(permission);

            return new PermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
            };
        }

        public async Task<PermissionResponse> UpdatePermission(Guid id, PermissionDto dto, string creatorName)
        {
            var permission = await _permissionRepository.GetPermissionById(id);
            if (permission == null)
                throw new KeyNotFoundException($"No se encontró el permiso con id {id}");

            permission.Name = dto.Name;
            permission.Description = dto.Description ?? "";
            permission.UpdatedAt = DateTime.UtcNow;
            permission.UpdatedBy = creatorName;

            await _permissionRepository.UpdatePermission(permission);

            return new PermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
            };
        }

        public async Task DeletePermission(Guid id)
        {
            var permission = await _permissionRepository.GetPermissionById(id);
            if (permission == null)
                throw new KeyNotFoundException($"No se encontró el permiso");

            await _permissionRepository.DeletePermission(id);
        }
    }
}