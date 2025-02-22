using System.ComponentModel.DataAnnotations;

namespace Backend.Data.DTOs.Note;

public class AddNoteDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
}
