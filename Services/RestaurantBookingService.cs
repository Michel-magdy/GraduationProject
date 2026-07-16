using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RestaurantBookingService : GenericService<RestaurantBooking>, IRestaurantBooking
{
    readonly private Context context;
    public RestaurantBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

    public void CancelBooking(int bookingId)
    {
        var booking = context.RestaurantBookings.Find(bookingId);
        if (booking != null)
        {
            booking.Status = BookingStatus.Cancelled;
            context.SaveChanges();
        }
    }

    public void ConfirmBooking(int bookingId)
    {
        var booking = context.RestaurantBookings.Find(bookingId);
        if (booking != null)
        {
            booking.Status = BookingStatus.Confirmed;
            context.SaveChanges();
        }
    }

    public IEnumerable<RestaurantBooking> GetBookingsByRestaurant(int restaurantId)
    {
        return context.RestaurantBookings
            .Include(b => b.Table)
            .Include(b => b.User)
            .Where(b => b.Table != null && b.Table.RestaurantId == restaurantId)
            .ToList();
    }

    public IEnumerable<RestaurantBooking> GetBookingsByUser(int userId)
    {
        return context.RestaurantBookings
            .Include(b => b.Table)
                .ThenInclude(t => t != null ? t.Restaurant : null)
            .Where(b => b.UserId == userId)
            .ToList();
    }

    public List<RestaurantBooking> GetRestaurantBookings()
    {
        return context.RestaurantBookings
            .Include(RB => RB.User)
            .Include(RB => RB.Table)
            .ToList();
    }
}