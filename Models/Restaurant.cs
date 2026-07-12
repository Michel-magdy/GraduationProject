using GraduationProject.Interfaces;

namespace GraduationProject.Models;

public class Restaurant : ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    public int BusinessId { get; set; }
    public Business? Business { get; set; }

    public ICollection<Table> Tables { get; set; } = new List<Table>();
}
