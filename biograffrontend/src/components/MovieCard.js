import React from "react";
export function MovieCard({ movie, onDelete }) {
  return (
    <div className="movie-card">
      <div className="movie-card__year">{movie.year || "-"}</div>
      <h2 className="movie-card__title">{movie.title}</h2>
      <p className="movie-card__genre">{movie.genre || "Okand genre"}</p>
      {movie.director && <p className="movie-card__director">Regi: {movie.director}</p>}
      {movie.description && <p className="movie-card__desc">{movie.description}</p>}
      {onDelete && <button className="movie-card__delete" onClick={() => onDelete(movie.id)}>X</button>}
    </div>
  );
}
