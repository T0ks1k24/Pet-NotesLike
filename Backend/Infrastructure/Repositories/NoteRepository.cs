using Backend.Data;
using Backend.Data.Models;
using Backend.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoteEntity>> GetAllByUserIdAsync(Guid id)
        {
            return await _context.Notes.AsNoTracking().Where(n => n.UserId == id).ToListAsync();
        }

        public async Task AddAsync(NoteEntity note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, NoteEntity note)
        {
            var existingNote = await _context.Notes.FindAsync(id);

            if (existingNote == null)
            {
                throw new KeyNotFoundException($"Note with ID {id} not found.");
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.UpdatedDate = DateTime.UtcNow;

            _context.Notes.Update(existingNote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingNote = await _context.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);

            if (existingNote != null)
            {
                _context.Notes.Remove(existingNote);
                await _context.SaveChangesAsync();
            }
        }
    }
}
