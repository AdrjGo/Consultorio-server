using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRoleRespository : IUserRoleRepository
    {
        private readonly DBContext _context;
        public UserRoleRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByIds(IEnumerable<Guid> ids)
        {
            return await _context.UserRoles
                .Where(ur => ids.Contains(ur.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByUserId(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> UpdateUserRoles(IEnumerable<UserRole> userRoles)
        {
            _context.UserRoles.UpdateRange(userRoles);
            await _context.SaveChangesAsync();
            return userRoles;
        }

        public async Task AssignRoleToUser(IEnumerable<UserRole> userRoles)
        {
            _context.UserRoles.AddRange(userRoles);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRoles(IEnumerable<UserRole> userRoles)
        {
            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
        }
    }
}