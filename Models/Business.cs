namespace GraduationProject.Models;

public class Business
{
    public int Id { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public User? Owner { get; set; }

    public Hotel? Hotel { get; set; }
    public Restaurant? Restaurant { get; set; }
    public CarRental? CarRental { get; set; }
    public Tour? Tour { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
