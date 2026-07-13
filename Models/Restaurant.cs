using System.ComponentModel.DataAnnotations;
using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class Restaurant : ISoftDelete
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<RestaurantReview> Reviews { get; set; } = new List<RestaurantReview>();

    public ICollection<Table> Tables { get; set; } = new List<Table>();
}
