using System.ComponentModel.DataAnnotations;

namespace Backend.Data.DTOs.Note;

public class AddNoteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
