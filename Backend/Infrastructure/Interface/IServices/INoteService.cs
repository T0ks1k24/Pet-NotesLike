using Backend.Data.DTOs.Note;

namespace Backend.Infrastructure.Interface.IServices
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllAsync();
        Task<NoteDto> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateNoteDto noteDto);
        Task DeleteAsync(Guid id);
        Task<Guid> AddNoteAsync(Guid userId, CreateNoteDto noteDto);
        Task<IEnumerable<NoteDto>> GetUserNotesAsync(Guid userId);

    }
}
