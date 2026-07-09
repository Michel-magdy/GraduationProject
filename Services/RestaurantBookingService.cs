using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class RestaurantBookingService : GenericService<RestaurantBooking>, IRestaurantBooking
{
    readonly private Context context;
    public RestaurantBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

}