using System.ComponentModel.DataAnnotations;
using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class Tour : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateTime TripDate { get; set; }
    [Range(100, 100000)]
    public decimal Price { get; set; }
    [Range(100, 1000)]
    public int MaxParticipants { get; set; }
    [Range(0, 1000)]
    public int CurrentParticipants { get; set; }
    public bool IsDeleted { get; set; } = false;

    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();
}
