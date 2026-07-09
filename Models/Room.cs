namespace GraduationProject.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Capacity { get; set; }
    public bool Available { get; set; }

    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public ICollection<HotelBooking> HotelBookings { get; set; } = new List<HotelBooking>();
}
