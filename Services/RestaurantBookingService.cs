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
        throw new NotImplementedException();
    }

    public void ConfirmBooking(int bookingId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<RestaurantBooking> GetBookingsByRestaurant(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<RestaurantBooking> GetBookingsByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public List<RestaurantBooking> GetRestaurantBookings()
    {
        return context.RestaurantBookings
            .Include(RB => RB.User)
            .Include(RB => RB.Table)
            .ToList();
    }
}