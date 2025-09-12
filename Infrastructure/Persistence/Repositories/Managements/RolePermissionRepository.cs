using Domain.Entities;
using Domain.Enum;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RolePermissionRespository
    {
        private readonly DBContext _context;
        public RolePermissionRespository(DBContext context)
        {
            _context = context;
        }

        public async Task AssignPermissionToRole(Guid roleId, Guid permissionId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return;
            var rolePermission = await _context.RolePermissions.FindAsync(roleId, permissionId);
            if (rolePermission == null)
            {
                var rolePermissionObject = new RolePermission()
                {
                    Id = Guid.CreateVersion7(),
                    RoleId = roleId,
                    PermissionId = permissionId,
                    CreatedBy = role.Name,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = role.Name,
                    UpdatedAt = DateTime.Now,
                    State = States.ACTIVE
                };
                _context.RolePermissions.Add(rolePermissionObject);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemovePermissionFromRole(Guid roleId, Guid permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}