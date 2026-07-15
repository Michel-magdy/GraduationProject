using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class CarRentalBookingService : GenericService<CarRentalBooking>, ICarRentalBooking
{
    readonly private Context context;
    public CarRentalBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

    public decimal CalculateTotalPrice(int carId, DateTime start, DateTime end)
    {
        var car = context.CarRentals.Find(carId);
        if (car == null || end <= start)
            return 0m;

        var days = (end.Date - start.Date).Days;
        return days > 0 ? days * car.PricePerDay : 0m;
    }

    public void CancelBooking(int bookingId)
    {
        Delete(bookingId);
    }

    public List<CarRentalBooking> GetBookings()
    {
        return context.CarRentalBookings
            .Include(b => b.Car)
            .Include(b => b.User)
            .ToList();
    }

    public IEnumerable<CarRentalBooking> GetBookingsByCar(int carId)
    {
        return context.CarRentalBookings
            .Include(b => b.Car)
            .Include(b => b.User)
            .Where(b => b.CarId == carId)
            .ToList();
    }

    public IEnumerable<CarRentalBooking> GetBookingsByUser(int userId)
    {
        return context.CarRentalBookings
            .Include(b => b.Car)
            .Include(b => b.User)
            .Where(b => b.UserId == userId)
            .ToList();
    }

    public CarRentalBooking? GetBookingWithDetails(int id)
    {
        return context.CarRentalBookings
            .Include(b => b.Car)
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id);
    }
}