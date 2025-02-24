"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { Login } from "../../../services/authService";
import styles from "./page.module.scss";
import LoginForm from "../../../components/Auth/LoginForm/LoginForm";

export default function LoginPage() {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState(null);
	const router = useRouter();

	const handleSubmit = async (e) => {
		e.preventDefault();
		setError(null);

		try {
			const response = await Login({ email, password });

			if (response?.userId) {
				localStorage.setItem("userId", response.userId);
				router.push("/note");
			} else {
				setError("Invalid email or password");
			}
		} catch (error) {
			setError(error.message || "Login failed. Please try again.");
		}
	};



return (
	<main className={styles.container}>
		<div className={styles.loginBox}>
			<h1 className={styles.title}>Login to Your Account</h1>
			<p className={styles.subtitle}>
				Welcome back! Please enter your credentials.
			</p>
			<LoginForm
				email={email}
				setEmail={setEmail}
				password={password}
				setPassword={setPassword}
				handleSubmit={handleSubmit}
				error={error}
			/>
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
