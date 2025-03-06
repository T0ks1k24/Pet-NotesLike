using Backend.Data.Models;

namespace Backend.Infrastructure.Interface.IRepositories
{
    public interface IUserRepository
    {
        Task Register(User user);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUserName(string username);
    }
}
