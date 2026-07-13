using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRoom : IService<Room>
{
    IEnumerable<Room> GetRoomsByHotel(int hotelId);

    IEnumerable<Room> GetAvailableRooms(int hotelId);

    bool IsRoomAvailable(int roomId);
}
