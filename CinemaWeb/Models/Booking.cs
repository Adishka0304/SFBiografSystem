namespace CinemaWeb.Models;

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string SalongNamn { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
}