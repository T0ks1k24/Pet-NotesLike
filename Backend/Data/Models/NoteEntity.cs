using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Models;

public class NoteEntity
{
    [Key]
    public Guid Id {  get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
