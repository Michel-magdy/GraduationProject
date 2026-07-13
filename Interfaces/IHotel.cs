using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotel : IService<Hotel>
{
    IEnumerable<Hotel> GetHotelsByBusiness(int businessId);

    Hotel? GetHotelWithRooms(int hotelId);

    Hotel? GetHotelFullDetails(int hotelId);
}
