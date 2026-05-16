namespace CinemaWeb.Models;

public class Salong
{
    public int Id { get; set; }
    public string Namn { get; set; } = string.Empty;
    public int AntalPlatser { get; set; }
    public string Typ { get; set; } = string.Empty;
}