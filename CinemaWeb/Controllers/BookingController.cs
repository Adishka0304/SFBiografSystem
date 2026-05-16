using CinemaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly HttpClient _http;
    private const string ApiUrl = "http://localhost:5024/api";

    public BookingController(IHttpClientFactory factory)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Account");
        var bookings = await _http.GetFromJsonAsync<List<Booking>>($"{ApiUrl}/Booking/user/{userId}");
        var movies = await _http.GetFromJsonAsync<List<Movie>>($"{ApiUrl}/Movie");
        ViewBag.Movies = movies;
        return View(bookings);
    }

    public async Task<IActionResult> Create(int movieId)
    {
        var movie = await _http.GetFromJsonAsync<Movie>($"{ApiUrl}/Movie/{movieId}");
        var salonger = await _http.GetFromJsonAsync<List<Salong>>("http://localhost:5105/api/Salong");
        ViewBag.Movie = movie;
        ViewBag.Salonger = salonger;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int movieId, string salongNamn)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var booking = new Booking
        {
            UserId = userId ?? 0,
            MovieId = movieId,
            SalongNamn = salongNamn,
            BookingDate = DateTime.Now
        };
        await _http.PostAsJsonAsync($"{ApiUrl}/Booking", booking);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _http.DeleteAsync($"{ApiUrl}/Booking/{id}");
        return RedirectToAction("Index");
    }
}