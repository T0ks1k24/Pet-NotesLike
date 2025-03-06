using Backend.Data.DTOs.User;
using Backend.Data.Models.Api;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public AccountController(JwtService jwtService, IUserService userService, IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userService = userService;
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequestModel request)
    {
        var existingUserEmail = await _userRepository.GetByEmail(request.Email);
        var existingUserUsername = await _userRepository.GetByUserName(request.UserName);
        
        if(existingUserEmail != null)
            return BadRequest("Користувач з таким email вже існує.");

        if(existingUserUsername != null)
            return BadRequest("Користувач з таким usrname вже існує.");

        await _userService.RegisterUser(new Register
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password
        });

        return Ok("Реєстрація успішна.");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
    {
        var result = await _jwtService.Authenticate(request);
        if (result is null)
            return Unauthorized();

        return result;
    }
}
