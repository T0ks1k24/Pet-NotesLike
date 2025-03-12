using Backend.Data.DTOs.Note;
using Backend.Data.Models;

namespace Backend.Infrastructure.Interface.IRepositories
{
    public interface INoteListRepository
    {
        Task AddAccessAsync(Guid ownerId, string username, Guid noteId, string accessLevel);
        Task<Dictionary<Guid, string>> GetUserAccessLevelsAsync(Guid userId, List<Guid> noteIds);
    }
}
