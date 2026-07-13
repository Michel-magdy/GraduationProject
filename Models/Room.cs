using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class Room
{
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;
    [Range(1, 100000)]

    public decimal Price { get; set; }
    [Range(1, 20)]

    public int Capacity { get; set; }

    [Required]
    public RoomStatus status { get; set; }

    [Required]
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public ICollection<HotelBooking> HotelBookings { get; set; } = new List<HotelBooking>();
}

public enum RoomStatus
{
    Available,
    Occupied,
    Maintenance,
    Inactive
}