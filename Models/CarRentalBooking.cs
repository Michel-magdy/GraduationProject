namespace GraduationProject.Models;

public class CarRentalBooking
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int CarId { get; set; }
    public CarRental? Car { get; set; }
}
