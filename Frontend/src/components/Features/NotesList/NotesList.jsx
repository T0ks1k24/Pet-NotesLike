"use client";

import { useEffect, useState } from "react";
import { GetAll, AddNote } from "../../../services/noteService";
import CardNote from "../../UI/CardNote/CardNote";
import styles from "./NotesList.module.scss";

export default function NotesList() {
  const [notes, setNotes] = useState([]);
  const [newNote, setNewNote] = useState({ title: "", content: "" });
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    async function fetchNotes() {
      const data = await GetAll();
      setNotes(data);
    }
    fetchNotes();
  }, []);

  const handleAddNote = async (e) => {
    e.preventDefault();

    const addedNote = await AddNote(newNote); // Додаємо нотатку

    // Якщо нотатку додано успішно
    if (addedNote) {
      setNotes((prevNotes) => [addedNote, ...prevNotes]); // Додаємо нову нотатку до списку
      setNewNote({ title: "", content: "" }); // Очищаємо форму
      setIsModalOpen(false); // Закриваємо модальне вікно
    } else {
      alert("Виникла помилка при додаванні нотатки. Спробуйте ще раз.");
    }
  };

  return (
    <div>
      <div className={styles.notes_container}>
        {notes.length > 0 ? (
          notes.map((note) => (
            <CardNote
              key={note.id}
              id={note.id}
              title={note.title}
              content={note.content}
            />
          ))
        ) : (
          <p>Нотаток поки немає.</p>
        )}
      </div>

      {/* Кнопка для додавання нотатки */}
      <button className={styles.add_note_button} onClick={() => setIsModalOpen(true)}>
        +
      </button>

      {/* Модальне вікно для додавання нотатки */}
      {isModalOpen && (
        <div className={styles.modal}>
          <div className={styles.modal_content}>
            <h2>Додати Нотатку</h2>
            <form onSubmit={handleAddNote}>
              <input
                type="text"
                value={newNote.title}
                onChange={(e) => setNewNote({ ...newNote, title: e.target.value })}
                placeholder="Title"
                required
              />
              <textarea
                value={newNote.content}
                onChange={(e) => setNewNote({ ...newNote, content: e.target.value })}
                placeholder="Content"
                required
              />
              <button type="submit">Додати</button>
              <button
                type="button"
                className={styles.cancel_button}
                onClick={() => setIsModalOpen(false)}
              >
                Скасувати
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
