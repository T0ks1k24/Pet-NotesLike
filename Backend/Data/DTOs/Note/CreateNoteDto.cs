﻿namespace Backend.Data.DTOs.Note
{
    public class CreateNoteDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = "Owner"; // Owner, Edit, View
    }
}
