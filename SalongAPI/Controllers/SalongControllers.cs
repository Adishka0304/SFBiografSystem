using Microsoft.AspNetCore.Mvc;
using SalongAPI.Models;

namespace SalongAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalongController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var salonger = new List<Salong>
        {
            new Salong { Id = 1, Namn = "Salong 1", AntalPlatser = 100, Typ = "Standard" },
            new Salong { Id = 2, Namn = "IMAX", AntalPlatser = 200, Typ = "IMAX" },
            new Salong { Id = 3, Namn = "3D-salen", AntalPlatser = 80, Typ = "3D" }
        };
        return Ok(salonger);
    }
}