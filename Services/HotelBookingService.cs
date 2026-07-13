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
        throw new NotImplementedException();
    }

    public void CancelBooking(int bookingId)
    {
        throw new NotImplementedException();
    }

    public void ConfirmBooking(int bookingId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<HotelBooking> GetBookingsByHotel(int hotelId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<HotelBooking> GetBookingsByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public List<HotelBooking> GetHotelBooking()
    {
        return context.HotelBookings
            .Include(Hb => Hb.Room)
            .Include(Hb => Hb.User)
            .ToList();
    }

}