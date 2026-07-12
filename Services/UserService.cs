using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class UserService : GenericService<User>, IUser
{
    readonly private Context context;
    public UserService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<User> GetUsers()
    {
        return context.Users
            .Include(user => user.Businesses)
            .Include(user => user.TourBookings)
            .Include(user => user.CarRentalBookings)
            .Include(user => user.HotelBookings)
            .Include(user => user.RestaurantBookings)
            .Include(user => user.Reviews)
            .Include(user => user.Role)
            .AsSplitQuery().ToList();
    }
}