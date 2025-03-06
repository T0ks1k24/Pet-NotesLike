using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Models
{
    public class NoteList
    {
        [Key]
        public int Id { get; set; }
        public Guid NoteId { get; set; }
        public Note? Note { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string AccessLevel { get; set; } = "Read"; // Owner, Read, Edit
    }
}
