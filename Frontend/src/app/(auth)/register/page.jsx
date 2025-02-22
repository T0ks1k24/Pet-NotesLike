import styles from "../page.module.scss";

export default function Register() {
  return (
    <main className={styles.container}>
      <div className={styles.registerBox}>
        <h1 className={styles.title}>Create an Account</h1>
        <p className={styles.subtitle}>Join us and explore new opportunities!</p>

        <form className={styles.form}>
          <div className={styles.formGroup}>
            <label htmlFor="username" className={styles.label}>Username</label>
            <input 
              type="text" 
              id="username" 
              name="username" 
              placeholder="Enter your username" 
              className={styles.input} 
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="email" className={styles.label}>Email</label>
            <input 
              type="email" 
              id="email" 
              name="email" 
              placeholder="Enter your email" 
              className={styles.input} 
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="password" className={styles.label}>Password</label>
            <input 
              type="password" 
              id="password" 
              name="password" 
              placeholder="Enter your password" 
              className={styles.input} 
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="confirmPassword" className={styles.label}>Confirm Password</label>
            <input 
              type="password" 
              id="confirmPassword" 
              name="confirmPassword" 
              placeholder="Confirm your password" 
              className={styles.input} 
            />
          </div>

          <button type="submit" className={styles.registerButton}>Sign Up</button>
        </form>

        <p className={styles.loginLink}>
          Already have an account? <a href="/login" className={styles.link}>Log in</a>
        </p>
      </div>
    </main>
  );
}
