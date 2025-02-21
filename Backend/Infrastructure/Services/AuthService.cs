using Backend.Application.Common;
using Backend.Data.DTOs.User;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Security;
using System.Text.RegularExpressions;

namespace Backend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> RegisterUserAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Email) || !registerDto.Email.Contains("@"))
                return Result.Failure("Некоректний email");

            if(!Regex.IsMatch(registerDto.UserName, "^[a-zA-Z0-9]+$"))
                return Result.Failure("Username не повинен містити спеціальних символів");

            if (registerDto.Password.Length < 6) 
                return Result.Failure("Пароль має бути мінімум 6 символів");

            if(registerDto.UserName.Length < 3 || string.IsNullOrWhiteSpace(registerDto.UserName))
                return Result.Failure("Username має бути більше 3 символів");

            var existingUser = await _authRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser == null)
                return Result.Failure("Користувач з таким email вже існує");


            var user = new UserEntity
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = _passwordHasher.Hash(registerDto.Password),
                CreatedDate = DateTime.UtcNow.Date,
            };

            await _authRepository.AddUserAsync(user);

            return Result.Success();

        }
        
    }
}
