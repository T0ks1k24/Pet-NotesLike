using Backend.Data.Models;
using Backend.Handlers;
using Microsoft.EntityFrameworkCore;


namespace Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<User> UserAccount { get; set; }
    public DbSet<NoteList> NoteLists { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
