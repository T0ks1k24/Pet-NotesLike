import styles from "../page.module.scss";

export default function Login() {
	return (
<main className={styles.container}>
			<div className={styles.loginBox}>
				<h1 className={styles.title}>Login to Your Account</h1>
				<p className={styles.subtitle}>
					Welcome back! Please enter your credentials.
				</p>
				<form className={styles.form}>
					<div className={styles.formGroup}>
						<label htmlFor="email" className={styles.label}>
							Email
						</label>
						<input
							type="email"
							id="email"
							name="email"
							placeholder="Enter your email"
							className={styles.input}
						/>
					</div>
					<div className={styles.formGroup}>
						<label htmlFor="password" className={styles.label}>
							Password
						</label>
						<input
							type="password"
							id="password"
							name="password"
							placeholder="Enter your password"
							className={styles.input}
						/>
					</div>

					<button type="submit" className={styles.loginButton}>
						Log In
					</button>
				</form>

				<p className={styles.registerLink}>
					Don't have an account?{" "}
					<a href="/register" className={styles.link}>
						Sign Up
					</a>
				</p>
			</div>
		</main>
	);
}
