using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services;

public class NoteListService : INoteListService
{
    private readonly INoteListRepository _noteListRepository;

    public NoteListService(INoteListRepository noteListRepository)
    {
        _noteListRepository = noteListRepository;
    }

    public async Task ShareNoteAsync(Guid ownerId, string username, Guid noteId, string accessLevel)
    {
        await _noteListRepository.AddAccessAsync(ownerId, username, noteId, accessLevel);
    }

    public async Task<Dictionary<Guid, string>> GetUserAccessLevelsAsync(Guid userId, List<Guid> noteIds)
    {
        return await _noteListRepository.GetUserAccessLevelsAsync(userId, noteIds);
    }

    public async Task<bool> UpdateAccessLevelAsync(Guid noteId, Guid userId, string newAccessLevel)
    {
        if(newAccessLevel == "Read" || newAccessLevel == "Edit")
        {
            return await _noteListRepository.UpdateAccessLevel(noteId, userId, newAccessLevel);
        }

        return false;
    }


}
