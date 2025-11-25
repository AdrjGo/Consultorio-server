using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleById(Guid id);
        Task<Role> GetRoleByName(string name);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<IEnumerable<Role>> GetRolesByUserId(Guid userId);
        Task<Role> CreateRole(Role role);
        Task<Role> CreateRoleWithPermissions(Role role, IEnumerable<RolePermission> rolePermissions);
        Task<Role> UpdateRole(Role role);
        Task DeleteRole(Guid id);
    }
}