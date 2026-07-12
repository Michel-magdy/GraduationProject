using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotelBooking : IService<HotelBooking>
{
    List<HotelBooking> GetHotelBooking();
}
