using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRespository : IRoleRepository
    {
        private readonly DBContext _context;
        public RoleRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            return await _context.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> GetRoleWithPermissionsById(Guid id)
        {
            return await _context.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => EF.Functions.ILike(r.Name, $"%{name}%"));
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles.Include(r => r.UserRoles).Include(r => r.RolePermissions).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesByUserId(Guid userId)
        {
            return await _context.Roles.Where(r => r.UserRoles.Any(u => u.UserId == userId)).ToListAsync();
        }
        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> CreateRoleWithPermissions(Role role, IEnumerable<RolePermission> rolePermissions)
        {
            _context.Roles.Add(role);
            _context.RolePermissions.AddRange(rolePermissions);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleWithPermissions(Role role, IEnumerable<RolePermission> rolePermissions)
        {
            _context.Roles.Update(role);
            _context.RolePermissions.RemoveRange(role.RolePermissions);
            _context.RolePermissions.AddRange(rolePermissions);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return;
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

    }
}