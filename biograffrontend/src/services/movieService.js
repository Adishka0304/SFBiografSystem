const BASE_URL = process.env.REACT_APP_API_URL || "http://localhost:5000";
export async function fetchMovies() {
  const r = await fetch(BASE_URL + "/api/movies");
  if (!r.ok) throw new Error("HTTP " + r.status);
  return r.json();
}
export async function createMovie(movie) {
  const r = await fetch(BASE_URL + "/api/movies", {
    method: "POST",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(movie)
  });
  if (!r.ok) throw new Error("HTTP " + r.status);
  return r.json();
}
export async function deleteMovie(id) {
  const r = await fetch(BASE_URL + "/api/movies/" + id, {method: "DELETE"});
  if (!r.ok) throw new Error("HTTP " + r.status);
}
