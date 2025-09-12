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

        public async Task<Role?> GetRoleById(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
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

        public async Task<Role> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
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