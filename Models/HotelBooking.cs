using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Models;

public class HotelBooking
{
    public int Id { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
    [Range(0, 100000)]
    public decimal TotalPrice { get; set; }
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
