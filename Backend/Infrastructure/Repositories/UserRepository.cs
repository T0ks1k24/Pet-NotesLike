using Backend.Data;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.UserAccount.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUserName(string username)
    {
        return await _context.UserAccount.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task Register(User user)
    {
        await _context.UserAccount.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
