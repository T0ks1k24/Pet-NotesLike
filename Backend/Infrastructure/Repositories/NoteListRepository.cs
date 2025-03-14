using Backend.Data;
using Backend.Data.DTOs.Note;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Infrastructure.Repositories;

public class NoteListRepository : INoteListRepository
{
    private readonly ApplicationDbContext _context;

    public NoteListRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.UserAccount.FirstOrDefaultAsync(u => u.UserName == username);
    }

    private async Task<bool> IsOwnerAsync(Guid userId, Guid noteId)
    {
        var isOwner = await _context.NoteLists.AnyAsync(n =>
        n.NoteId == noteId &&
        n.UserId == userId &&
        n.AccessLevel == "Owner");

        return isOwner;
    }

    public async Task AddAccessAsync(Guid ownerId, string username, Guid noteId, string accessLevel)
    {
        if (accessLevel != "Read" && accessLevel != "Edit")
            throw new ArgumentException("Неправильний рівень доступу.");

        var user = await GetUserByUsernameAsync(username);
        if (user == null)
            throw new ArgumentException("Користувача не знайдено.");

        if (!await IsOwnerAsync(ownerId, noteId))
            throw new UnauthorizedAccessException("Тільки власник може ділитися нотаткою.");

        var existingAccess = await _context.NoteLists
        .FirstOrDefaultAsync(nl => nl.UserId == user.Id && nl.NoteId == noteId);

        if (existingAccess != null)
        {
            if (existingAccess.AccessLevel == accessLevel)
                throw new InvalidOperationException("Цей рівень доступу вже встановлений.");
            existingAccess.AccessLevel = accessLevel;
        }
        else
        {
            var newAccess = new NoteList
            {
                UserId = user.Id,
                NoteId = noteId,
                AccessLevel = accessLevel
            };
            await _context.NoteLists.AddAsync(newAccess);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Dictionary<Guid, string>> GetUserAccessLevelsAsync(Guid userId, List<Guid> noteIds)
    {
        return await _context.NoteLists
            .Where(nl => noteIds.Contains(nl.NoteId) && nl.UserId == userId)
            .ToDictionaryAsync(nl => nl.NoteId, nl => nl.AccessLevel);
    }

    public async Task<bool> UpdateAccessLevel(Guid noteId, Guid userId, string newAccessLevel)
    {
        var noteList = await _context.NoteLists
            .Where(nl => nl.NoteId == noteId && nl.UserId == userId && nl.AccessLevel != "Owner")
            .FirstOrDefaultAsync();

        if (noteList == null) return false;

        noteList.AccessLevel = newAccessLevel;
        await _context.SaveChangesAsync();
        return true;

    }
}