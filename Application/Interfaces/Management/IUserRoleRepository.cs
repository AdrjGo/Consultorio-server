using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetUserRolesByIds(IEnumerable<Guid> ids);
        Task AssignRoleToUser(IEnumerable<UserRole> userRoles);
        Task RemoveUserRoles(IEnumerable<UserRole> userRoles);
    }
}