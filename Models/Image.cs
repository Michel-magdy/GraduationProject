namespace GraduationProject.Models;

public class Image
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = string.Empty;

    public int BusinessId { get; set; }
    public Business? Business { get; set; }
}
