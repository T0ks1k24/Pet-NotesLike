using Backend.Application.Common;
using Backend.Data.DTOs.User;

namespace Backend.Infrastructure.Interface.IServices;

public interface IAuthService
{
    Task<Result> RegisterUserAsync(RegisterDto registerDto);
    Task<Result> LoginUserAsync(LoginDto loginDto);
}
