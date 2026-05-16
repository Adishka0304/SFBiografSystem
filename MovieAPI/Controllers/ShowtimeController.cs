using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowtimeController : ControllerBase
{
    private readonly MovieContext _context;

    public ShowtimeController(MovieContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Showtime> showtimes = await _context.Showtimes.ToListAsync();
        return Ok(showtimes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        Showtime? showtime = await _context.Showtimes.FindAsync(id);
        if (showtime == null) return NotFound();
        return Ok(showtime);
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetByMovie(int movieId)
    {
        List<Showtime> showtimes = await _context.Showtimes
            .Where(s => s.MovieId == movieId)
            .ToListAsync();
        return Ok(showtimes);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Showtime showtime)
    {
        _context.Showtimes.Add(showtime);
        await _context.SaveChangesAsync();
        return Ok(showtime);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Showtime? showtime = await _context.Showtimes.FindAsync(id);
        if (showtime == null) return NotFound();
        _context.Showtimes.Remove(showtime);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}