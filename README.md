# SF Cinema - Biografhanteringssystem

## Vad systemet gör
Ett digitalt biografhanteringssystem som låter användare se filmer, boka biljetter och hantera visningstider. Administratörer kan hantera filmutbud och se bokningsstatistik.

## Systemets delar
- **CinemaWeb** – ASP.NET Core MVC webbplats (frontend)
- **MovieAPI** – Web API med SQLite-databas (CRUD för filmer, bokningar, användare)
- **SalongAPI** – Web API med hårdkodad data (salonger)

## Live-URLer (Azure)
- Webbplats: https://sfcinemaweb.azurewebsites.net
- MovieAPI: https://sfmovieapi.azurewebsites.net
- SalongAPI: https://sfsalongapi.azurewebsites.net

## Inloggning
### Admin
- Användarnamn: admin
- Lösenord: cinema123

### Vanlig användare
Skapa konto via "Skapa konto" på webbplatsen

## API-nyckel (SalongAPI)
SalongAPI är skyddad med API-nyckel. Skicka nyckeln i header:
X-Api-Key: sfcinema-secret-key-2026

## Köra lokalt
1. Klona repot
2. Öppna i Rider
3. Starta MovieAPI, SalongAPI och CinemaWeb
4. Gå till http://localhost:5209

## Teknologier
- ASP.NET Core MVC (.NET 10)
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core + SQLite
- Bootstrap + custom CSS
