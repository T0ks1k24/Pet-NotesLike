using Backend.Data.Models;

namespace Backend.Infrastructure.Interface.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserEntity user);
    }
}
