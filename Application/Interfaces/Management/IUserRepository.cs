using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<User?> GetUserByName(string name);
        Task<User?> LoginUser(string email, string password);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> ChangeState(Guid id);
        Task<User> DeleteUser(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetIsActive(Guid id);
    }
}