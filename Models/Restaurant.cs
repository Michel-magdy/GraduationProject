namespace GraduationProject.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<Table> Tables { get; set; } = new List<Table>();
}
