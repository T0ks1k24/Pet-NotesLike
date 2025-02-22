using Backend.Data.DTOs.Note;

namespace Backend.Infrastructure.Interface.IServices
{
    public interface INoteService
    {
        Task<List<NoteDto>> GetAllByUserIdAsync(Guid id);
        Task AddAsync(AddNoteDto noteDto);
        Task UpdateAsync(Guid id, UpdateNoteDto noteDto);
        Task DeleteAsync(Guid id);
    }
}
