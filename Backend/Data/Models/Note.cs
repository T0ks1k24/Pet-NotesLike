using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Models;

public class Note
{
    [Key]
    public Guid Id {  get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public ICollection<NoteList> NoteLists { get; set; } = new List<NoteList>();
}
