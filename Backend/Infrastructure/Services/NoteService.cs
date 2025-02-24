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

    public async Task<IEnumerable<NoteDto>> GetAllAsync()
    {
        var notes = await _noteRepository.GetAllAsync();
        return notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            CreatedDate = n.CreatedDate,
            UpdatedDate = n.UpdatedDate
        }).ToList();
    }

    public async Task<NoteDto?> GetByIdAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedDate = note.CreatedDate,
            UpdatedDate = note.UpdatedDate, 
        };
    }

    public async Task AddAsync(AddNoteDto noteDto)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = noteDto.Title,
            Content = noteDto.Content,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await _noteRepository.AddAsync(note);
    }

    public async Task UpdateAsync(Guid id, UpdateNoteDto noteDto)
    {
        var existingNote = await _noteRepository.GetByIdAsync(id);

        if (existingNote == null)
        {
            throw new KeyNotFoundException($"Note with ID {id} not found.");
        }

        if(!string.IsNullOrEmpty(noteDto.Title))
            existingNote.Title = noteDto.Title;

        if(!string.IsNullOrEmpty(noteDto.Content))
            existingNote.Content = noteDto.Content;

        existingNote.UpdatedDate = DateTime.UtcNow;

        await _noteRepository.UpdateAsync(existingNote);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _noteRepository.DeleteAsync(id);
    }

    
}
