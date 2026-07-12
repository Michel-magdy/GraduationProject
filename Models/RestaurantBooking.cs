using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class RestaurantBooking
{
    public int Id { get; set; }
    [Required]
    public DateTime ReservationTime { get; set; }
    [Range(1, 30)]
    public int Guests { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int TableId { get; set; }
    public Table? Table { get; set; }
}
