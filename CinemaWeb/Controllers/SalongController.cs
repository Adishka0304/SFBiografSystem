using CinemaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

public class SalongController : Controller
{
    private readonly HttpClient _http;
    private readonly string _salongApiUrl;

    public SalongController(IHttpClientFactory factory, IConfiguration config)
    {
        _http = factory.CreateClient();
        _http.DefaultRequestHeaders.Add("X-Api-Key", config["SalongApiKey"]);
        _salongApiUrl = config["SalongApiUrl"] ?? "https://sfsalongapi.azurewebsites.net/api/Salong";
    }

    public async Task<IActionResult> Index()
    {
        var salonger = await _http.GetFromJsonAsync<List<Salong>>(_salongApiUrl);
        return View(salonger);
    }
}