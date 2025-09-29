using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;

namespace Application.Services
{
    public class RolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task AssignPermissionsToRole(IEnumerable<RolePermissionDto> rolePermissions, string creatorName)
        {
            var rolePermission = rolePermissions.Select(rp => new RolePermission
            {
                Id = Guid.CreateVersion7(),
                RoleId = rp.RoleId,
                PermissionId = rp.PermissionId,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            }).ToList();

            await _rolePermissionRepository.AssignPermissionToRole(rolePermission);
        }

        public async Task RemovePermissionsFromRole(IEnumerable<Guid> rolePermissionIds)
        {
            var rolePermissions = await _rolePermissionRepository.GetRolePermissionsByIds(rolePermissionIds);

            if (!rolePermissions.Any())
                throw new KeyNotFoundException("No se encontraron permisos para eliminar.");

            await _rolePermissionRepository.RemovePermissionFromRole(rolePermissionIds);
        }
    }
}