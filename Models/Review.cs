namespace GraduationProject.Models;

public class Review
{
    public int Id { get; set; }
    public int Rate { get; set; }
    public string Comment { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User? User { get; set; }

    public int BusinessId { get; set; }
    public Business? Business { get; set; }
}
