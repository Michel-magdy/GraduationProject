using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class Business : ISoftDelete
{
    public int Id { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;


    public int OwnerId { get; set; }
    public User? Owner { get; set; }

    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public ICollection<CarRental> CarRentals { get; set; } = new List<CarRental>();
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
