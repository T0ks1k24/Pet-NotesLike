using Backend.Data.Models;

namespace Backend.Infrastructure.Interface.IRepositories
{
    public interface INoteRepository
    {
        Task<List<NoteEntity>> GetAllByUserIdAsync(Guid id);
        Task AddAsync(NoteEntity note);
        Task UpdateAsync(Guid id, NoteEntity note);
        Task DeleteAsync(Guid id);
    }
}
