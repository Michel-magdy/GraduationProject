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

    public void CancelBooking(int bookingId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TourBooking> GetBookingsByTour(int tourId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TourBooking> GetBookingsByUser(int userId)
    {
        throw new NotImplementedException();
    }
}