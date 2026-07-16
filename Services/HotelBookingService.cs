using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class HotelBookingService : GenericService<HotelBooking>, IHotelBooking
{
    readonly private Context context;
    public HotelBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

    public decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut)
    {
        var room = context.Rooms.Find(roomId);
        if (room == null) return 0;
        
        int days = (int)(checkOut.Date - checkIn.Date).TotalDays;
        if (days <= 0) days = 1;
        
        return room.Price * days;
    }

    public void CancelBooking(int bookingId)
    {
        var booking = context.HotelBookings.Find(bookingId);
        if (booking != null)
        {
            booking.Status = BookingStatus.Cancelled;
            context.SaveChanges();
        }
    }

    public void ConfirmBooking(int bookingId)
    {
        var booking = context.HotelBookings.Find(bookingId);
        if (booking != null)
        {
            booking.Status = BookingStatus.Confirmed;
            context.SaveChanges();
        }
    }

    public IEnumerable<HotelBooking> GetBookingsByHotel(int hotelId)
    {
        return context.HotelBookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Where(b => b.Room != null && b.Room.HotelId == hotelId)
            .ToList();
    }

    public IEnumerable<HotelBooking> GetBookingsByUser(int userId)
    {
        return context.HotelBookings
            .Include(b => b.Room)
                .ThenInclude(r => r != null ? r.Hotel : null)
            .Where(b => b.UserId == userId)
            .ToList();
    }

    public List<HotelBooking> GetHotelBooking()
    {
        return context.HotelBookings
            .Include(Hb => Hb.Room)
            .Include(Hb => Hb.User)
            .ToList();
    }

}