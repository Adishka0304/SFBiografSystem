using CinemaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

public class MovieController : Controller
{
    private readonly HttpClient _http;
    private const string MovieApiUrl = "http://localhost:5024/api/Movie";

    public MovieController(IHttpClientFactory factory)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _http.GetFromJsonAsync<List<Movie>>(MovieApiUrl);
        return View(movies);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await _http.GetFromJsonAsync<Movie>($"{MovieApiUrl}/{id}");
        return View(movie);
    }

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        await _http.PostAsJsonAsync(MovieApiUrl, movie);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _http.GetFromJsonAsync<Movie>($"{MovieApiUrl}/{id}");
        return View(movie);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Movie movie)
    {
        await _http.PutAsJsonAsync($"{MovieApiUrl}/{id}", movie);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _http.DeleteAsync($"{MovieApiUrl}/{id}");
        return RedirectToAction(nameof(Index));
    }
}