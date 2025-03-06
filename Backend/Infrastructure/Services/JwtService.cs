using Backend.Data;
using Backend.Data.Models.Api;
using Backend.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Infrastructure.Services;

public class JwtService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public JwtService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseModel?> Authenticate(LoginRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var userAccount = await _context.UserAccount.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (userAccount is null || !PasswordHashHandler.VerifyPassword(request.Password, userAccount.Password))
            return null;

        var issuer = _configuration["JwtOptions:Issuer"];
        var audience = _configuration["JwtOptions:Audience"];
        var key = _configuration["JwtOptions:Key"];
        var tokenValidityMins = _configuration.GetValue<int>("JwtOptions:TokenValidityMins");

        var issuedAt = DateTime.UtcNow;
        var notBefore = issuedAt.AddSeconds(-1);
        var tokenExpiryTimeStamp = issuedAt.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Name, request.Email),
            new Claim(ClaimTypes.Role, userAccount.Role)
        }),
            IssuedAt = issuedAt,                       
            NotBefore = notBefore,                     
            Expires = tokenExpiryTimeStamp,  
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature
            ),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseModel
        {
            AccessToken = accessToken,
            Email = request.Email,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
        };
    }

}
