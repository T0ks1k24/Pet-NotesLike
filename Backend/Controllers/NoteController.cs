﻿using Backend.Data.DTOs.Note;
using Backend.Infrastructure.Interface.IRepositories;
using Backend.Infrastructure.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll() => Ok(await _noteService.GetAllAsync());
         
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetById(Guid id) => Ok(await _noteService.GetByIdAsync(id));

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteDto noteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _noteService.UpdateAsync(id, noteDto);
            return NoContent();
        }
        catch (KeyNotFoundException) { 
            return NotFound($"Note with ID {id} not found.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _noteService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("user/{userId}")]
    public async Task<IActionResult> AddNote(Guid userId, [FromBody] CreateNoteDto noteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var noteId = await _noteService.AddNoteAsync(userId, noteDto);
            return CreatedAtAction(nameof(GetUserNotes), new { userId = userId }, new { id = noteId });
        } 
        catch (Exception ex)
        {
            return StatusCode(500, $"Помилка: {ex.Message}");
        }
    }

    [HttpGet("user/{userId}/notes")]
    public async Task<IActionResult> GetUserNotes(Guid userId)
    {
        var userNotes = await _noteService.GetUserNotesAsync(userId);

        if(userNotes == null || !userNotes.Any())
        {
            return NotFound($"No notes found for user with ID {userId}.");
        }

        return Ok(userNotes);
    }

}
