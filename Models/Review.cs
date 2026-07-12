using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class Review
{
    public int Id { get; set; }
    [Range(1, 5)]
    public int Rate { get; set; }
    [Required]
    [StringLength(200)]
    public string Comment { get; set; } = string.Empty;
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }
    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }
}
