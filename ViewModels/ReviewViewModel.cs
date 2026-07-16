using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models; // Reusing Models namespace to avoid using another namespace in ReviewController if not imported

public class ReviewViewModel
{
    [Required]
    public int EntityId { get; set; }
    
    [Required]
    public string EntityType { get; set; } = string.Empty;

    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rate { get; set; }

    [Required]
    [StringLength(500)]
    public string Comment { get; set; } = string.Empty;
}
