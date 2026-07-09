namespace GraduationProject.Models;

public class RestaurantBooking
{
    public int Id { get; set; }
    public DateTime ReservationTime { get; set; }
    public int Guests { get; set; }
    public string Status { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User? User { get; set; }

    public int TableId { get; set; }
    public Table? Table { get; set; }
}
