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
        var user = context.Users.Find(userId);
        if (user != null)
        {
            user.RoleId = roleId;
            context.SaveChanges();
        }
    }

    public bool EmailExists(string email)
    {
        return context.Users.Any(u => u.Email == email && !u.IsDeleted);
    }

    public List<User>? GetAllUsers()
    {
        return context.Users
            .Include(u => u.Role)
            .Include(u => u.Businesses)
            .Where(u => !u.IsDeleted)
            .ToList();

    }

    public IEnumerable<User> GetCustomers()
    {
        return context.Users
            .Include(u => u.Role)
            .Where(u => u.Role!.Name == "Customer" && !u.IsDeleted)
            .ToList();
    }

    public List<User>? GetOwners()
    {
        return context.Users
            .Include(u => u.Role)
            .Where(u => u.Role!.Name == "Owner" && !u.IsDeleted).ToList();
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
        return context.Users
            .Include(u => u.Role)
            .Include(u => u.Businesses)
            .Include(u => u.HotelBookings)
            .Include(u => u.RestaurantBookings)
            .Include(u => u.CarRentalBookings)
            .Include(u => u.TourBookings)
            .AsSplitQuery()
            .FirstOrDefault(u => u.Id == id && !u.IsDeleted);
    }

    public User? Login(string email, string password)
    {
        var user = context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == email && !u.IsDeleted);

        if (user == null)
            return null;

        // Simple password comparison (stored as plain hash for this project)
        if (user.PasswordHash != password)
            return null;

        return user;
    }
}
