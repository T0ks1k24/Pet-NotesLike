const API_URL = "https://localhost:7040/api/Note"

export async function GetNotesByUserId(){
  try {
    const userId = localStorage.getItem("userId"); 
    if (!userId) throw new Error("User ID not found");

    const response = await fetch(`${API_URL}/${userId}`);
    if(!response.ok) throw new Error("Failed to fetch notes");

    return await response.json();
  } catch (error) {
    console.error("Error loading products:", error);
    return [];
  }
}

export async function createNote(note) {
	try {
		const response = await fetch(API_URL, {
			method: "POST",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify(note),
			mode: "cors",
		});
		if (!response.ok) throw new Error("Failed to create note");
		return await response.json();
	} catch (error) {
		console.error("Error creating note:", error);
	}
}

export async function deleteProduct(NoteId) {
	try {
		const response = await fetch(`${API_URL}/${NoteId}`, {
			method: "DELETE",
		});
		if (!response.ok) throw new Error("Failed to delete note");
		return true;
	} catch (error) {
		console.error("Error deleting note:", error);
		return false;
	} 
}