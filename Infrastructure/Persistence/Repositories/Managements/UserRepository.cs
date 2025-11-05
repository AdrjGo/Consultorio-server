using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly DBContext _context;
        public UserRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<User?> LoginUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Person.Email.Value == email && u.Password == password);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users.Include(u => u.Person).FirstAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByName(string name)
        {
            return await _context.Users.Include(u => u.Person).FirstOrDefaultAsync(u => EF.Functions.ILike(u.Person.Name, $"%{name}%") | EF.Functions.ILike(u.Person.LastName, $"%{name}%"));
        }

        public async Task<User?> GetUserByCi(string ci)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Person.Ci == ci);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.Person).Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> ChangeState(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            user.State = States.ACTIVE | States.INACTIVE;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Person.Email.Value == email);
        }


        public async Task<User> GetIsActive(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.State != States.ACTIVE);
        }
    }
}