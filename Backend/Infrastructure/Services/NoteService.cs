using Backend.Data.DTOs.Note;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;

namespace Backend.Infrastructure.Services;

public class NoteService :INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<List<NoteDto>> GetAllByUserIdAsync(Guid userId)
    {
        var notes = await _noteRepository.GetAllByUserIdAsync(userId);
        return notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            CreatedDate = n.CreatedDate,
            UpdatedDate = n.UpdatedDate
        }).ToList();
    }

    public async Task AddAsync(AddNoteDto noteDto)
    {
        var note = new NoteEntity
        {
            Id = Guid.NewGuid(),
            Title = noteDto.Title,
            Content = noteDto.Content,
            UserId = noteDto.UserId,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await _noteRepository.AddAsync(note);
    }

    public async Task UpdateAsync(Guid id, UpdateNoteDto noteDto)
    {
        var note = new NoteEntity
        {
            Title = noteDto.Title,
            Content = noteDto.Content
        };

        await _noteRepository.UpdateAsync(id, note);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _noteRepository.DeleteAsync(id);
    }

    
  
}
