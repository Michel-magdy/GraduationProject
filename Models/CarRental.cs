using System.ComponentModel.DataAnnotations;
using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class CarRental : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Model { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string Brand { get; set; } = string.Empty;
    [Range(1000, 100000)]
    public decimal PricePerDay { get; set; }
    [StringLength(300)]
    public string Image { get; set; } = string.Empty;
    public bool Available { get; set; }
    public bool IsDeleted { get; set; } = false;
    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<CarRentalBooking> CarRentalBookings { get; set; } = new List<CarRentalBooking>();
}
