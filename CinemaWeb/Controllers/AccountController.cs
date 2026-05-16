using System.Security.Claims;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _http;
    private const string ApiUrl = "http://localhost:5024/api";

    public AccountController(IHttpClientFactory factory)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        _http = new HttpClient(handler);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Admin-login
        if (username == "admin" && password == "cinema123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Movie");
        }

        // Vanlig användare
        var user = new User { Username = username, Password = password };
        var response = await _http.PostAsJsonAsync($"{ApiUrl}/User/login", user);
        if (response.IsSuccessStatusCode)
        {
            var loggedIn = await response.Content.ReadFromJsonAsync<User>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loggedIn!.Username),
                new Claim("UserId", loggedIn.Id.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            HttpContext.Session.SetInt32("UserId", loggedIn.Id);
            return RedirectToAction("Index", "Movie");
        }

        ViewBag.Error = "Fel användarnamn eller lösenord";
        return View();
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password, string email)
    {
        var user = new User { Username = username, Password = password, Email = email };
        var response = await _http.PostAsJsonAsync($"{ApiUrl}/User/register", user);
        if (response.IsSuccessStatusCode)
        {
            ViewBag.Success = "Konto skapat! Logga in.";
            return View("Login");
        }
        ViewBag.Error = "Användarnamnet är redan taget";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}