import React from "react";
export function SearchBar({ value, onChange }) {
  return (
    <div className="search">
      <input className="search__input" type="text" placeholder="Sok..." value={value} onChange={e => onChange(e.target.value)} />
      {value && <button className="search__clear" onClick={() => onChange("")}>X</button>}
    </div>
  );
}
