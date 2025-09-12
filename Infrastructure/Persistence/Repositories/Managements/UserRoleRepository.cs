using Domain.Entities;
using Domain.Enum;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRoleRespository
    {
        private readonly DBContext _context;
        public UserRoleRespository(DBContext context)
        {
            _context = context;
        }

        public async Task AssignRoleToUser(Guid userId, Guid roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;
            var userRoles = await _context.UserRoles.FindAsync(userId, roleId);
            if (userRoles == null)
            {
                var userRoleObject = new UserRole()
                {
                    Id = Guid.CreateVersion7(),
                    UserId = userId,
                    RoleId = roleId,
                    CreatedBy = user.Person.Name + " " + user.Person.LastName,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = user.Person.Name + " " + user.Person.LastName,
                    UpdatedAt = DateTime.Now,
                    State = States.ACTIVE
                };
                _context.UserRoles.Add(userRoleObject);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleFromUser(Guid userId, Guid roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}