const API_URL = "https://localhost:7040/api/Auth";

export async function Register(user) {
	try {
		const response = await fetch(`${API_URL}/register`, {
			method: "POST",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify(user),
			mode: "cors",
		});
		if (!response.ok) throw new Error("Failed to register user");
		return await response.json();
	} catch (error) {
		console.error("Error registering user:", error);
	}
}

export async function Login(user) {
	try {
		const response = await fetch(`${API_URL}/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(user),
      mode: "cors",
		});

		if (!response.ok) {
			const errorText = await response.text();
			throw new Error(`Failed to login: ${errorText}`);
		}

		const data = await response.json();
		localStorage.setItem("userId", data.userId); 
		return data;
	} catch (error) {
		console.error("Error logging in:", error);
		throw error;
	}
}


