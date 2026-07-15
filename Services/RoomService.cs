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
        return context.Rooms
            .Where(r => r.HotelId == hotelId && r.status == RoomStatus.Available)
            .Include(r => r.Hotel)
            .ToList();
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
        return context.Rooms
            .Where(r => r.HotelId == hotelId)
            .Include(r => r.Hotel)
            .Include(r => r.HotelBookings)
            .ToList();
    }

    public bool IsRoomAvailable(int roomId)
    {
        var room = context.Rooms.Find(roomId);
        return room != null && room.status == RoomStatus.Available;
    }

    public Room? GetRoomWithDetails(int roomId)
    {
        return context.Rooms
            .Include(r => r.Hotel)
            .Include(r => r.HotelBookings)
                .ThenInclude(hb => hb.User)
            .FirstOrDefault(r => r.Id == roomId);
    }
}
