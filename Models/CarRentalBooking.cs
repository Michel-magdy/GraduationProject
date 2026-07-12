using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Models;

public class CarRentalBooking
{
    public int Id { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Range(0, 10000)]
    public decimal TotalPrice { get; set; }
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int CarId { get; set; }
    public CarRental? Car { get; set; }
}
