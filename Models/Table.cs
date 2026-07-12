using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class Table
{
    public int Id { get; set; }
    [Required]
    [StringLength(10)]
    public string TableNumber { get; set; } = string.Empty;
    [Range(1, 20)]
    public int Capacity { get; set; }
    public bool Available { get; set; }
    [Required]

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public ICollection<RestaurantBooking> RestaurantBookings { get; set; } = new List<RestaurantBooking>();
}
