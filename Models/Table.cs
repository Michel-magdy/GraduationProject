namespace GraduationProject.Models;

public class Table
{
    public int Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool Available { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public ICollection<RestaurantBooking> RestaurantBookings { get; set; } = new List<RestaurantBooking>();
}
