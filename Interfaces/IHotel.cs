using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotel : IService<Hotel>
{
    List<Hotel> GetHotels();
}
