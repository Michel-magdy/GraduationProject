namespace GraduationProject.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public decimal Price { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }

    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();
}
