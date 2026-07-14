using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotel : IService<Hotel>
{
    IEnumerable<Hotel> GetHotelsByBusiness(int businessId);

    Hotel? GetHotelWithRooms(int hotelId);

    Hotel? GetHotelFullDetails(int hotelId);

    List<Hotel> GetHotels();

    void UpdateHotel(Hotel hotel);

    void AddImage(int hotelId, string imagePath);

    void UpdateImage(int hotelId, int imageId, string imagePath);

    void DeleteImage(int hotelId, int imageId);

}
