using CinemaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly HttpClient _http;
    private const string ApiUrl = "http://localhost:5024/api";

    public AdminController(IHttpClientFactory factory)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _http.GetFromJsonAsync<List<Movie>>($"{ApiUrl}/Movie");
        var bookings = await _http.GetFromJsonAsync<List<Booking>>($"{ApiUrl}/Booking");
        var users = await _http.GetFromJsonAsync<List<User>>($"{ApiUrl}/User");
        ViewBag.Movies = movies;
        ViewBag.Users = users;
        return View(bookings);
    }
}