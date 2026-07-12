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

    public List<RestaurantBooking> GetRestaurantBookings()
    {
        return context.RestaurantBookings
            .Include(RB => RB.User)
            .Include(RB => RB.Table)
            .ToList();
    }
}