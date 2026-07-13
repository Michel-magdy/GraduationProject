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

    public void ChangeRole(int userId, int roleId)
    {
        throw new NotImplementedException();
    }

    public bool EmailExists(string email)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetCustomers()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetOwners()
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        return context.Users
            .Where(user => !user.IsDeleted)
            .Include(user => user.Businesses)
            .Include(user => user.TourBookings)
            .Include(user => user.CarRentalBookings)
            .Include(user => user.HotelBookings)
            .Include(user => user.RestaurantBookings)
            .Include(user => user.Role)
            .AsSplitQuery().ToList();
    }

    public User? GetUserWithDetails(int id)
    {
        throw new NotImplementedException();
    }

    public User? Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}
