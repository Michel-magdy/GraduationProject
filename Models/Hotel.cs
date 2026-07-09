namespace GraduationProject.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
