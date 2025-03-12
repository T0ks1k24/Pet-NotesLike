namespace Backend.Infrastructure.Interface.IServices
{
    public interface INoteListService
    {
        Task ShareNoteAsync(Guid ownerId, string username, Guid noteId, string accessLevel);
        Task<Dictionary<Guid, string>> GetUserAccessLevelsAsync(Guid userId, List<Guid> noteIds);
    }
}
