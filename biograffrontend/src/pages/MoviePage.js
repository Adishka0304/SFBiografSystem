import React, { useState } from "react";
import { useMovies } from "../hooks/useMovies";
import { MovieCard } from "../components/MovieCard";
import { AddMovieForm } from "../components/AddMovieForm";
import { SearchBar } from "../components/SearchBar";
export function MoviePage() {
  const { movies, loading, error, reload, removeMovie } = useMovies();
  const [search, setSearch] = useState("");
  const [showForm, setShowForm] = useState(false);
  const filtered = movies.filter(m => {
    const q = search.toLowerCase();
    return m.title?.toLowerCase().includes(q) || m.genre?.toLowerCase().includes(q) || m.director?.toLowerCase().includes(q);
  });
  async function handleDelete(id) {
    if (window.confirm("Ta bort filmen?")) {
      try { await removeMovie(id); }
      catch { alert("Kunde inte ta bort."); }
    }
  }
  return (
    <div className="page">
      <header className="header">
        <div className="header__inner">
          <h1 className="header__title">MovieVault</h1>
          <button
            className={"header__add-btn" + (showForm ? " header__add-btn--active" : "")}
            onClick={() => setShowForm(v => !v)}
          >
            {showForm ? "Stang" : "+ Ny film"}
          </button>
        </div>
      </header>
      <main className="main">
        {showForm && <AddMovieForm onAdded={() => { reload(); setShowForm(false); }} />}
        <SearchBar value={search} onChange={setSearch} />
        {loading && <div className="state-msg">Hamtar filmer...</div>}
        {error && <div className="state-msg state-msg--error">{error} <button className="retry-btn" onClick={reload}>Forsok igen</button></div>}
        {!loading && !error && filtered.length === 0 && <div className="state-msg">{search ? "Inga traffar" : "Inga filmer annu!"}</div>}
        <div className="grid">
          {filtered.map(movie => <MovieCard key={movie.id} movie={movie} onDelete={handleDelete} />)}
        </div>
        {!loading && !error && <p className="count">{filtered.length} av {movies.length} filmer</p>}
      </main>
    </div>
  );
}
