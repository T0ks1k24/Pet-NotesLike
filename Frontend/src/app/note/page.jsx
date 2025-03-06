import NotesList from "../../components/Features/NotesList/NotesList";
import styles from "./page.module.css"

export default function Note() {
  return (
    <main>
			<div className={styles.head_div}>
				<h1 className={styles.h1}>Notes</h1>
			</div>
			<NotesList/>
		</main>
  );
}