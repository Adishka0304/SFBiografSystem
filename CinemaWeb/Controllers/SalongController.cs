using CinemaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

public class SalongController : Controller
{
    private readonly HttpClient _http;
    private const string SalongApiUrl = "http://localhost:5105/api/Salong";

    public SalongController(IHttpClientFactory factory)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
    }

    public async Task<IActionResult> Index()
    {
        var salonger = await _http.GetFromJsonAsync<List<Salong>>(SalongApiUrl);
        return View(salonger);
    }
}