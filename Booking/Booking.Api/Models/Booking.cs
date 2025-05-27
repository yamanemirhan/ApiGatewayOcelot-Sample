namespace Booking.Api.Models;

public class Booking
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string FlightNumber { get; set; }
    public DateTime Date { get; set; }
}