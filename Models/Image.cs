using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class Image
{
    public int Id { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public int? BusinessId { get; set; }
    public Business? Business { get; set; }

    public int? HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public int? TourId { get; set; }
    public Tour? Tour { get; set; }

    public int? CarRentalId { get; set; }
    public CarRental? CarRental { get; set; }
}
