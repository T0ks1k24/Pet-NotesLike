import styles from "./InputField.module.scss"

export default function InputField({
	label,
	type,
	id,
	value,
	onChange,
	placeholder,
}) {
	return (
		<div className={styles.formGroup}>
			<label htmlFor={id} className={styles.label}>
				{label}
			</label>
			<input
				type={type}
				id={id}
				name={id}
				placeholder={placeholder}
				className={styles.input}
				value={value}
				onChange={onChange}

			/>
		</div>
	);
}
