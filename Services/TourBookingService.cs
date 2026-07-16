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

    public IEnumerable<TourBooking> GetBookings()
    {
        return context.TourBookings
            .Include(b => b.Tour)
            .Include(b => b.User)
            .ToList();
    }

    public TourBooking? GetBookingWithDetails(int id)
    {
        return context.TourBookings
            .Include(b => b.Tour)
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id);
    }

    public IEnumerable<TourBooking> GetBookingsByTour(int tourId)
    {
        return context.TourBookings
            .Where(b => b.TourId == tourId)
            .Include(b => b.User)
            .ToList();
    }

    public IEnumerable<TourBooking> GetBookingsByUser(int userId)
    {
        return context.TourBookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Tour)
            .ToList();
    }

    public void CancelBooking(int bookingId)
    {
        // TourBooking has no Status field, so "cancel" removes the booking outright.
        var booking = context.TourBookings.Find(bookingId);

        if (booking == null)
            return;

        context.TourBookings.Remove(booking);
        context.SaveChanges();
    }
}