using Application.Dto;
using Application.Responses;
using Application.Utils;
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

        public async Task<IEnumerable<RolePermissionResponse>> GetAllPermissions()
        {
            var permissions = await _permissionRepository.GetAllPermissions();
            if (permissions == null)
                throw new KeyNotFoundException($"No se encontró ningún permiso");

            return permissions.GroupBy(rp => rp.Key)
                    .Select(g => new RolePermissionResponse
                    {
                        // Id = Guid.NewGuid(),
                        Key = g.Key,
                        Permissions = g.Select(rp => new PermissionResponse
                        {
                            Id = rp.Id,
                            Name = rp.Name,
                            Description = rp.Description
                        }).ToList()
                    })
                    .ToList();
        }

        public async Task<PermissionResponse> GetPermissionByName(string name)
        {
            var permission = await _permissionRepository.GetPermissionByName(name);

            if (permission == null)
                throw new KeyNotFoundException($"No se encontró el permiso con nombre: {name}");

            return new PermissionResponse
            {
                Id = permission.Id,
                Key = permission.Key,
                Name = permission.Name,
                Description = permission.Description,
            };
        }

        public async Task<PermissionMessageResponse> CreatePermission(PermissionDto dto, string creatorName)
        {
            var permission = new Permission
            {
                Id = Guid.CreateVersion7(),
                Key = dto.Key,
                Name = dto.Name,
                Description = dto.Description ?? "",
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            await _permissionRepository.CreatePermission(permission);

            return new PermissionMessageResponse
            {
                Message = "Permiso creado correctamente",
            };
        }

        public async Task<PermissionMessageResponse> UpdatePermission(Guid id, PermissionDto dto, string creatorName)
        {
            var permission = await _permissionRepository.GetPermissionById(id);
            if (permission == null)
                throw new KeyNotFoundException($"No se encontró el permiso con id {id}");

            permission.Name = dto.Name;
            permission.Description = dto.Description ?? "";
            permission.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            permission.UpdatedBy = creatorName;

            await _permissionRepository.UpdatePermission(permission);

            return new PermissionMessageResponse
            {
                Message = "Permiso actualizado correctamente",
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