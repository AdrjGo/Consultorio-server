using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetUserRolesByIds(IEnumerable<Guid> ids);
        Task<IEnumerable<UserRole>> GetUserRolesByUserId(Guid userId);
        Task<IEnumerable<UserRole>> UpdateUserRoles(IEnumerable<UserRole> userRoles);
        Task AssignRoleToUser(IEnumerable<UserRole> userRoles);
        Task RemoveUserRoles(IEnumerable<UserRole> userRoles);
    }
}