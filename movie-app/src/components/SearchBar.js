import React from "react";

export function SearchBar({ value, onChange }) {
  return (
    <div className="search">
      <span className="search__icon">🔍</span>
      <input
        className="search__input"
        type="text"
        placeholder="Sök på titel, genre eller regissör…"
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
      {value && (
        <button className="search__clear" onClick={() => onChange("")}>✕</button>
      )}
    </div>
  );
}
