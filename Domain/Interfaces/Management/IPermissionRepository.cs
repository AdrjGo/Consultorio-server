using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetPermissionById(Guid id);
        Task<IEnumerable<Permission>> GetAllPermissions();
        Task<Permission> CreatePermission(Permission permission);
        Task<Permission> UpdatePermission(Permission permission);
        Task DeletePermission(Guid id);
    }
}