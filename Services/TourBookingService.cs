using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

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
        Delete(bookingId);
    }

    public IEnumerable<TourBooking> GetBookingsByTour(int tourId)
    {
        return context.TourBookings
            .Include(b => b.Tour)
            .Include(b => b.User)
            .Where(b => b.TourId == tourId)
            .ToList();
    }

    public IEnumerable<TourBooking> GetBookingsByUser(int userId)
    {
        return context.TourBookings
            .Include(b => b.Tour)
            .Where(b => b.UserId == userId)
            .ToList();
    }
}