using Backend.Data.DTOs.Note;
using Backend.Data.Models;

namespace Backend.Infrastructure.Interface.IRepositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();
        Task<Note?> GetByIdAsync(Guid id);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(Guid id);
        Task<List<NoteDto>> GetUserNotesAsync(Guid userId);
        Task AddNoteListAsync(NoteList noteList);
        
    }
}
