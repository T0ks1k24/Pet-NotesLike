import styles from "./CardNote.module.css"

export default function CardNote({ title, content }) {
  return (
    <section className={styles.card_note}>
      <h2 className={styles.h2}>{title}</h2>
      <p className={styles.p}>{content}</p>
    </section>
  );
}