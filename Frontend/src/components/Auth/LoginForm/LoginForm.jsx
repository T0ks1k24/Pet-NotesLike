import InputField from "../../UI/InputField/InputField";
import styles from "./LoginForm.module.scss";

export default function LoginForm({
	email,
	setEmail,
	password,
	setPassword,
	handleSubmit,
	error,
}) {
	const handleInputChange = (setter) => (event) => {
		setter(event.target.value);
	};

	return (
		<form className={styles.form} onSubmit={handleSubmit}>
			<InputField
				lable="Email"
				type="email"
				id="email"
				value={email}
				onChange={handleInputChange(setEmail)}
				placeholder="Enter your email"
			/>
			<InputField
				lable="Password"
				type="password"
				id="password"
				value={password}
				onChange={handleInputChange(setPassword)}
				placeholder="Enter your password"
			/>

      {error && <p className={styles.error}>{error}</p>}
      <button type="submit" className={styles.loginButton}>Login</button>
		</form>
	);
}
