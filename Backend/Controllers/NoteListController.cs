using Backend.Data.Models.Api;
using Backend.Infrastructure.Interface.IServices;
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
    }
}
