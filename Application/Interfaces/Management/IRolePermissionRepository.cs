using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission>> GetRolePermissionsByIds(IEnumerable<Guid> ids);
        Task AssignPermissionToRole(IEnumerable<RolePermission> rolePermissions);
        Task RemovePermissionFromRole(IEnumerable<Guid> id);
    }
}