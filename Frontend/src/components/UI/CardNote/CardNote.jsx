import { useState, useEffect } from "react";
import { UpdateNote } from "../../../services/noteService"; // Import the UpdateNote function
import styles from "./CardNote.module.css";

export default function CardNote({ id, title, content, onSave, onDelete }) {
  const [isEditing, setIsEditing] = useState(false);
  const [noteTitle, setNoteTitle] = useState(title);
  const [noteContent, setNoteContent] = useState(content);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (isEditing && !event.target.closest("." + styles.card_note)) {
        handleSave();
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [isEditing]);

  const handleSave = async () => {
    if (isEditing && (title !== noteTitle || content !== noteContent)) {
      try {
        // Use UpdateNote to send the PATCH request
        const updatedNote = await UpdateNote(id, { title: noteTitle, content: noteContent });
        
        // If the update is successful, call onSave
        if (updatedNote) {
          onSave(id, { title: noteTitle, content: noteContent });
        }
      } catch (error) {
        console.error('Error updating the note:', error);
      }
    }
    setIsEditing(false);
  };

  return (
    <section className={styles.card_note} onClick={() => setIsEditing(true)}>
      {isEditing ? (
        <>
          <input
            className={styles.input}
            value={noteTitle}
            onChange={(e) => setNoteTitle(e.target.value)}
            autoFocus
          />
          <textarea
            className={styles.textarea}
            value={noteContent}
            onChange={(e) => setNoteContent(e.target.value)}
            autoFocus
          />
        </>
      ) : (
        <>
          <h2 className={styles.h2}>{noteTitle}</h2>
          <p className={styles.p}>{noteContent}</p>
          <button className={styles.button} onClick={() => onDelete(id)}>Delete</button>
        </>
      )}
    </section>
  );
}
