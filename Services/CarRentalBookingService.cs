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
        throw new NotImplementedException();
    }

    public void CancelBooking(int bookingId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CarRentalBooking> GetBookingsByCar(int carId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CarRentalBooking> GetBookingsByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public List<CarRentalBooking> GetCarRentalBookings()
    {
        return context.CarRentalBookings
            .Include(Crb => Crb.Car)
            .Include(Crb => Crb.User)
            .ToList();
    }
}