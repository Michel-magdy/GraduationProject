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

    public bool Available { get; set; }
    public bool IsDeleted { get; set; } = false;
    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public List<Image> Images { get; set; } = new List<Image>();
    public List<CarRentalReview> Reviews { get; set; } = new List<CarRentalReview>();
    public List<CarRentalBooking> CarRentalBookings { get; set; } = new List<CarRentalBooking>();
}
