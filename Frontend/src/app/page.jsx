import Link from "next/link";
import styles from "./page.module.css";

export default function Index() {
	return (
		<main>
			<div className={styles.head_div}>
				<h1 className={styles.h1}>Home</h1>
			</div>
			<Link href={"/note"} className={styles.link}>
				<button className={styles.button}>My Notes</button>
			</Link>
		</main>
	);
}
