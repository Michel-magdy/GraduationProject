using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class CarRental : ISoftDelete
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool Available { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<CarRentalBooking> CarRentalBookings { get; set; } = new List<CarRentalBooking>();
}
