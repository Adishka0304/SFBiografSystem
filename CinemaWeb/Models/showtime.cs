namespace CinemaWeb.Models;

public class Showtime
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string SalongNamn { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }
}