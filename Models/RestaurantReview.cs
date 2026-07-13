using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class RestaurantReview
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rate { get; set; }

    [Required]
    [StringLength(500)]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}