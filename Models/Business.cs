using System.ComponentModel.DataAnnotations;
using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class Business : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2)]

    public string BusinessName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    public BusinessStatus status { get; set; }

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    [Required]
    public int OwnerId { get; set; }
    public User? Owner { get; set; }

    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public ICollection<CarRental> CarRentals { get; set; } = new List<CarRental>();
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public ICollection<Image> Images { get; set; } = new List<Image>();
}
public enum BusinessStatus
{
    Pending,
    Approved,
    Rejected
}