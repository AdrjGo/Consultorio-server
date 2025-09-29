using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DBContext _context;
        public PermissionRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Permission?> GetPermissionById(Guid id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission?> GetPermissionByName(string name)
        {
            return await _context.Permissions.FirstOrDefaultAsync(p => EF.Functions.ILike(p.Name, $"%{name}%"));
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> CreatePermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task DeletePermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null) return;
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }
}