namespace Backend.Data.Models.Api
{
    public class ShareNoteRequest
    {
        public Guid OwnerId { get; set; }
        public string Username { get; set; }
        public Guid NoteId { get; set; }
        public string AccessLevel { get; set; } = "Read";
    }
}
