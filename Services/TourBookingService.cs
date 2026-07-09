using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class TourBookingService : GenericService<TourBooking>, ITourBooking
{
    readonly private Context context;
    public TourBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

}