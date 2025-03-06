using Backend.Data.DTOs.Note;
using Backend.Data.DTOs.User;
using Backend.Data.Models;
using Backend.Handlers;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Repositories;

namespace Backend.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterUser(Register user)
    {
        var account = new User
        {
            Id = Guid.NewGuid(),
            UserName = user.UserName,
            Email = user.Email,
            Password = PasswordHashHandler.HashPassword(user.Password),
            Role = "User"
        };

        await _userRepository.Register(account);
    }
}
