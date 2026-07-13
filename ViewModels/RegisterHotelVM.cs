using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels;

public class RegisterHotelVM
{

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string BusinessName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Hotel
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    // Images
    public List<IFormFile>? Images { get; set; }
}
