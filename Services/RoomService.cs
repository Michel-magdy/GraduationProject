using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RoomService : GenericService<Room>, IRoom
{
    readonly private Context context;
    public RoomService(Context _context) : base(_context)
    {
        context = _context;
    }

    public IEnumerable<Room> GetAvailableRooms(int hotelId)
    {
        throw new NotImplementedException();
    }

    public List<Room> GetRooms()
    {
        return context.Rooms
            .Include(R => R.Hotel)
            .Include(R => R.HotelBookings)
            .ToList();
    }

    public IEnumerable<Room> GetRoomsByHotel(int hotelId)
    {
        throw new NotImplementedException();
    }

    public bool IsRoomAvailable(int roomId)
    {
        throw new NotImplementedException();
    }
}