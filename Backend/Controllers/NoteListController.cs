using Backend.Data.Models.Api;
using Backend.Infrastructure.Interface.IServices;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteListController : ControllerBase
    {
        private readonly INoteListService _noteListService;

        public NoteListController(INoteListService noteListService)
        {
            _noteListService = noteListService;
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareNote([FromBody] ShareNoteRequest request)
        {
            await _noteListService.ShareNoteAsync(request.OwnerId, request.Username, request.NoteId, request.AccessLevel);
            return Ok("Нотатка успішно поділена.");
        }

        [HttpPut("{noteId}/users/{userId}/access")]
        public async Task<IActionResult> UpdateAccessLevel(Guid noteId, Guid userId, [FromBody] string newAccessLevel)
        {
            var success = await _noteListService.UpdateAccessLevelAsync(noteId, userId, newAccessLevel);

            if (!success) return NotFound("Note or user not found, or access level is 'Owner'.");

            return Ok("Access level updated successfully.");
        }
    }
}
