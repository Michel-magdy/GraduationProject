using System.ComponentModel.DataAnnotations;
using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class User : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Min Length is 2 Characters")]
    public string FullName { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;


    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public ICollection<Business> Businesses { get; set; } = new List<Business>();
    public ICollection<HotelBooking> HotelBookings { get; set; } = new List<HotelBooking>();
    public ICollection<RestaurantBooking> RestaurantBookings { get; set; } = new List<RestaurantBooking>();
    public ICollection<CarRentalBooking> CarRentalBookings { get; set; } = new List<CarRentalBooking>();
    public ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
