using Backend.Data.DTOs.Note;

namespace Backend.Infrastructure.Interface.IServices
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllAsync();
        Task<NoteDto> GetByIdAsync(Guid id);
        Task AddAsync(AddNoteDto noteDto);
        Task UpdateAsync(Guid id, UpdateNoteDto noteDto);
        Task DeleteAsync(Guid id);
    }
}
