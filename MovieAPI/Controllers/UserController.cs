using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;

namespace MovieAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MovieContext _context;

    public UserController(MovieContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        var exists = await _context.Users.AnyAsync(u => u.Username == user.Username);
        if (exists) return BadRequest("Användarnamn redan taget");
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        var found = await _context.Users.FirstOrDefaultAsync(u => 
            u.Username == user.Username && u.Password == user.Password);
        if (found == null) return Unauthorized("Fel användarnamn eller lösenord");
        return Ok(found);
    }
}