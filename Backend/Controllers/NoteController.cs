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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll()
    {
        var notes = await _noteService.GetAllAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetById(Guid id)
    {
        var note = await _noteService.GetByIdAsync(id);
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddNoteDto noteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _noteService.AddAsync(noteDto);

        return CreatedAtAction(nameof(GetById), new { id = noteDto.Id });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteDto noteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _noteService.UpdateAsync(id, noteDto);
            return NoContent(); 
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Note with ID {id} not found.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _noteService.DeleteAsync(id);
        return NoContent();
    }

}
