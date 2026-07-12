using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class TourBooking
{
    public int Id { get; set; }
    [Range(1, 20)]
    public int Persons { get; set; }
    [Range(1, 200000)]
    public decimal TotalPrice { get; set; }
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int TourId { get; set; }
    public Tour? Tour { get; set; }
}
