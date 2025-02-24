"use client";

import { useEffect, useState } from "react";
import { GetNotesByUserId } from "../../../services/noteService";
import CardNote from "../../UI/CardNote/CardNote";
import styles from "./NotesList.module.css";

export default function NotesList() {
  const [notes, setNotes] = useState([]);

  useEffect(() => {
    async function fetchNotes(){
      const data = await GetNotesByUserId();
      setNotes(data)
    }
    fetchNotes();
  }, [])

  return (
    <div className={styles.notes_container}>
      {notes.length > 0 ? (
        notes.map((note)=> (
          <CardNote key={note.id} title={note.title} content={note.content} />
        ))
      ) : (
        <p>Нотаток поки немає.</p>
      )}
    </div>
  )
}