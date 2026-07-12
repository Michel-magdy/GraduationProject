using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models;

public class Image
{
    public int Id { get; set; }
    [Required]
    [StringLength(300)]
    public string ImagePath { get; set; } = string.Empty;

    [Required]
    public int BusinessId { get; set; }
    public Business? Business { get; set; }
}
