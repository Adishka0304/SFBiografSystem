import React, { useState } from "react";
import { createMovie } from "../services/movieService";

const EMPTY = { title: "", genre: "", director: "", year: "", description: "" };

export function AddMovieForm({ onAdded }) {
  const [form, setForm] = useState(EMPTY);
  const [saving, setSaving] = useState(false);
  const [err, setErr] = useState(null);

  function handleChange(e) {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    if (!form.title.trim()) return;
    setSaving(true);
    setErr(null);
    try {
      const movie = {
        ...form,
        year: form.year ? parseInt(form.year, 10) : null,
      };
      await createMovie(movie);
      setForm(EMPTY);
      onAdded();
    } catch {
      setErr("Kunde inte spara filmen. Försök igen.");
    } finally {
      setSaving(false);
    }
  }

  return (
    <form className="add-form" onSubmit={handleSubmit} noValidate>
      <h2 className="add-form__title">Lägg till film</h2>
      {err && <p className="add-form__error">{err}</p>}
      <div className="add-form__row">
        <input className="add-form__input" name="title" value={form.ti        <input className="add-form__iner="Titel *" required />
        <input className="add-form__input" name="year" value={form.year} onChange={handleChange} placeholder="År" type="number" min="1888" max="2099" />
      </div>
      <div className="add-form__row">
        <inp        <inp        <inp        ame="genre" value={form.genre} onChange={handleChange} placehol        <in />
        <input className="add-form__input" name="director" value={form.director} onChange={handleChange} placeholder="Regissör" />
      </      </      </      </     e="add-form__input add-form__textarea" name="description" value={form.description} onChange={handleChange} placeholder="Beskrivning" rows={3} />
      <button className="add-form__submit" type="submit" disabled={saving}>
        {saving ? "Sp        {saving ? "Sp        {saving ? "Sp   </form>
  );
}
