using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RolePermissionRespository : IRolePermissionRepository
    {
        private readonly DBContext _context;
        public RolePermissionRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolePermission>> GetRolePermissionsByIds(IEnumerable<Guid> ids)
        {
            return await _context.RolePermissions
                .Where(rp => ids.Contains(rp.Id))
                .ToListAsync();
        }

        public async Task AssignPermissionToRole(IEnumerable<RolePermission> rolePermissions)
        {
            _context.RolePermissions.AddRange(rolePermissions);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePermissionFromRole(IEnumerable<Guid> id)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null) return;
            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
        }
    }
}