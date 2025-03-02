const API_URL = "https://localhost:7040/api/Note";

async function fetchData(url, options = {}) {
	try {
		const response = await fetch(url, options);
		if (!response.ok)
			throw new Error(`HTTP error! Status: ${response.status}`);

		if (response.status == 204) {
			return true;
		}

		const contentType = response.headers.get("Content-Type");

		if (!contentType || !contentType.includes("application/json")) {
			return null;
		}

		return await response.json();
	} catch (error) {
		console.error("Error loading products:", error);
		return false;
	}
}

export const GetAll = () => fetchData(API_URL);
export const GetById = (id) => fetchData(`${API_URL}/${id}`);

export const AddNote = (note) =>
	fetchData(API_URL, {
		method: "POST",
		headers: { "Content-Type": "application/json" },
		body: JSON.stringify(note),
		mode: "cors",
	});

export const UpdateNote = (id, note) =>
	fetchData(`${API_URL}/${id}`, {
		method: "PATCH",
		headers: { "Content-Type": "application/json" },
		body: JSON.stringify(note),
		mode: "cors",
	});

export const DeleteNote = (id) =>
	fetchData(`${API_URL}/${id}`, { method: "DELETE" })
		.then((response) => response.ok)
		.catch((error) => {
			console.error("Error deleting note:", error);
			return false;
		});
