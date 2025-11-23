

using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly DBContext _context;

        public UserPermissionService(DBContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasPermissionAsync(Guid userId, string permission)
        {
            return await _context.UserRoles
               .Where(ur => ur.UserId == userId)
               .AnyAsync(ur =>
                   _context.RolePermissions
                       .Any(rp => rp.RoleId == ur.RoleId &&
                                  _context.Permissions.Any(p =>
                                      p.Id == rp.PermissionId)));
        }
    }
}