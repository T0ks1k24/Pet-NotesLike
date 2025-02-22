using Backend.Application.Common;
using Backend.Data.DTOs.User;
using Backend.Infrastructure.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterUserAsync(registerDto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok("Реєстрація успішна");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result  = await _authService.LoginUserAsync(loginDto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok("Вхід успішний");
    }


}
