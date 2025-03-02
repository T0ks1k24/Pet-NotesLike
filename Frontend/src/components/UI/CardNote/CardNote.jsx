import { useState } from "react";
import { DeleteNote, UpdateNote } from "../../../services/noteService";
import styles from "./CardNote.module.scss";

export default function CardNote({ id, title, content, onNoteDeleted }) {
  const [isEditing, setIsEditing] = useState(false);
  const [noteTitle, setNoteTitle] = useState(title);
  const [noteContent, setNoteContent] = useState(content);

  const handleSave = async () => {
    const updatedNote = {title: noteTitle, content: noteContent}
    const result = await UpdateNote(id, updatedNote)
    if (result){
      setIsEditing(false);
    }
  };

  const handleDelete = async () => {
    const isDeleted = await DeleteNote(id);
    if(isDeleted && onNoteDeleted){
      onNoteDeleted(id);
    }
  };

  return (
    <section className={styles.card_note}>
      {isEditing ? (
        <>
          <input
            className={styles.input}
            value={noteTitle}
            onChange={(e) => setNoteTitle(e.target.value)}
          />
          <textarea
            className={styles.textarea}
            value={noteContent}
            onChange={(e) => setNoteContent(e.target.value)}
          />
          <div className={styles.buttons}>
            <button className={styles.save_btn} onClick={handleSave}>Зберегти</button>
            <button className={styles.cancel_btn} onClick={() => setIsEditing(false)}>Скасувати</button>
          </div>
        </>
      ) : (
        <>
          <h2 className={styles.h2}>{noteTitle}</h2>
          <p className={styles.p}>{noteContent}</p>
          <div className={styles.buttons}>
            <button className={styles.edit_btn} onClick={() => setIsEditing(true)}>Редагувати</button>
            <button className={styles.delete_btn} onClick={handleDelete}>Видалити</button>
          </div>
        </>
      )}
    </section>
  );
}
