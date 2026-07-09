namespace GraduationProject.Models;

public class HotelBooking
{
    public int Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
