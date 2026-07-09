using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class CarRentalBookingService : GenericService<CarRentalBooking>, ICarRentalBooking
{
    readonly private Context context;
    public CarRentalBookingService(Context _context) : base(_context)
    {
        context = _context;
    }

}