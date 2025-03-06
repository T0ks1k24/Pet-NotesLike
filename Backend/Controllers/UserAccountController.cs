using Backend.Data;
using Backend.Data.Models;
using Backend.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserAccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<List<User>> GetAll() => await _context.UserAccount.ToListAsync();
   

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<User?> GetById(Guid id) =>
        await _context.UserAccount.FirstOrDefaultAsync(u => u.Id == id);

    [HttpGet("by-email")]
    [Authorize]
    public async Task<User?> GetByEmail([FromQuery] string email) =>
        await _context.UserAccount.FirstOrDefaultAsync(u => u.Email == email);
    

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult> Update([FromBody] User user)
    {
        if (user.Id == Guid.Empty)
        {
            return BadRequest("User ID is required");
        }

        var existingUser = await _context.UserAccount.FindAsync(user.Id);
        if (existingUser == null) return NotFound("User not found");
        
        if (!string.IsNullOrWhiteSpace(user.UserName))
            existingUser.UserName = user.UserName;
        
        if (!string.IsNullOrWhiteSpace(user.Email))
            existingUser.Email = user.Email;

        if (!string.IsNullOrWhiteSpace(user.Password) && !PasswordHashHandler.VerifyPassword(user.Password, existingUser.Password))
            existingUser.Password = PasswordHashHandler.HashPassword(user.Password);

        _context.UserAccount.Update(existingUser);
        await _context.SaveChangesAsync();

        return Ok("User updated successfully");
    }

}
