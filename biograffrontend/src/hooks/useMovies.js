import { useState, useEffect, useCallback } from "react";
import { fetchMovies, deleteMovie } from "../services/movieService";
export function useMovies() {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const loadMovies = useCallback(async () => {
    setLoading(true);
    setError(null);
    try { setMovies(await fetchMovies()); }
    catch { setError("Kunde inte hamta filmer."); }
    finally { setLoading(false); }
  }, []);
  useEffect(() => { loadMovies(); }, [loadMovies]);
  const removeMovie = useCallback(async (id) => {
    await deleteMovie(id);
    setMovies(prev => prev.filter(m => m.id !== id));
  }, []);
  return { movies, loading, error, reload: loadMovies, removeMovie };
}
