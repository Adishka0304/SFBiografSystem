using CinemaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

public class SalongController : Controller
{
    private readonly HttpClient _http;
    private readonly string _salongApiUrl;

    public SalongController(IHttpClientFactory factory, IConfiguration config)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
        _http.DefaultRequestHeaders.Add("X-Api-Key", config["SalongApiKey"]);
        _salongApiUrl = config["SalongApiUrl"] ?? "http://localhost:5105/api/Salong";
    }

    public async Task<IActionResult> Index()
    {
        var salonger = await _http.GetFromJsonAsync<List<Salong>>(_salongApiUrl);
        return View(salonger);
    }
}