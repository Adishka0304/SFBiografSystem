const BASE_URL = process.env.REACT_APP_API_URL || "http://localhost:5000";

export async function fetchMovies() {
  const response = await fetch(`${BASE_URL}/api/movies`);
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  return response.json();
}

export async function fetchMovieById(id) {
  const response = await fetch(`${BASE_URL}/api/movies/${id}`);
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  return response.json();
}

export async function createMovie(movie) {
  const response = await fetch(`${BASE_URL}/api/movies`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(movie),
  });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  return response.json();
}

export async function deleteMovie(id) {
  const response = await fetch(`${BASE_URL}/api/movies/${id}`, {
    method: "DELETE",
  });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
}
