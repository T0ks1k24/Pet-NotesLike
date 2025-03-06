using Backend.Data.DTOs.Note;
using Backend.Data.DTOs.User;

namespace Backend.Infrastructure.Interface.IServices
{
    public interface IUserService
    {
        Task RegisterUser(Register user);
    }
}
