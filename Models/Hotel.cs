using GraduationProject.Interfaces;
using System.ComponentModel.DataAnnotations;
namespace GraduationProject.Models;

public class Hotel : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    [Required]

    public int BusinessId { get; set; }
    public Business? Business { get; set; }
    public List<Image> Images { get; set; } = new List<Image>();

    public List<HotelReview> Reviews { get; set; } = new List<HotelReview>();
    public List<Room> Rooms { get; set; } = new List<Room>();
}
