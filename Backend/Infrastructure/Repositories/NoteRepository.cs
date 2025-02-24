using Backend.Data;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllAsync() => 
        await _context.Notes.AsNoTracking().ToListAsync();
    
    public async Task<Note?> GetByIdAsync(Guid id) => 
        await _context.Notes.AsNoTracking().FirstOrDefaultAsync(n=> n.Id == id);


    public async Task AddAsync(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Note note)
    {
        var existingNote = await _context.Notes.FindAsync(note.Id);

        if (existingNote == null)
        {
            throw new KeyNotFoundException($"Note with ID {note.Id} not found.");
        }

        if(!string.IsNullOrEmpty(note.Title))
            existingNote.Title = note.Title;

        if(!string.IsNullOrEmpty(note.Content))
            existingNote.Content = note.Content;
           
        existingNote.UpdatedDate = DateTime.UtcNow;

        _context.Notes.Update(existingNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

        if (existingNote != null)
        {
            _context.Notes.Remove(existingNote);
            await _context.SaveChangesAsync();
        }
    }
}
