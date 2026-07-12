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

    public List<CarRentalBooking> GetCarRentalBookings()
    {
        return context.CarRentalBookings
            .Include(Crb => Crb.Car)
            .Include(Crb => Crb.User)
            .ToList();
    }
}