using Backend.Data.DTOs.Note;
using Backend.Infrastructure.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<NoteDto>>> GetAllByUserId(Guid userId)
    {
        var notes = await _noteService.GetAllByUserIdAsync(userId);
        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddNoteDto noteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _noteService.AddAsync(noteDto);
        return CreatedAtAction(nameof(GetAllByUserId), new { userId = noteDto.UserId }, noteDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteDto noteDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        await _noteService.UpdateAsync(id, noteDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _noteService.DeleteAsync(id);
        return NoContent();
    }

}
