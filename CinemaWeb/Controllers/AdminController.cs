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
        var showtimes = await _http.GetFromJsonAsync<List<Showtime>>($"{ApiUrl}/Showtime");
        ViewBag.Movies = movies;
        ViewBag.Users = users;
        ViewBag.Showtimes = showtimes;
        return View(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShowtime(int movieId, string salongNamn, DateTime startTime, decimal price)
    {
        var showtime = new Showtime
        {
            MovieId = movieId,
            SalongNamn = salongNamn,
            StartTime = startTime,
            Price = price
        };
        await _http.PostAsJsonAsync($"{ApiUrl}/Showtime", showtime);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteShowtime(int id)
    {
        await _http.DeleteAsync($"{ApiUrl}/Showtime/{id}");
        return RedirectToAction("Index");
    }
}