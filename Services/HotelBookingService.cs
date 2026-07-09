using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class HotelBookingService : GenericService<HotelBooking>, IHotelBooking
{
    readonly private Context context;
    public HotelBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

}