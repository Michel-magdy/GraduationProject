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

    public List<HotelBooking> GetHotelBooking()
    {
        return context.HotelBookings
            .Include(Hb => Hb.Room)
            .Include(Hb => Hb.User)
            .ToList();
    }

}