namespace GraduationProject.Models;

public class TourBooking
{
    public int Id { get; set; }
    public int Persons { get; set; }
    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int TourId { get; set; }
    public Tour? Tour { get; set; }
}
