using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class CarRentalService : GenericService<CarRental>, ICarRental
{
    readonly private Context context;
    public CarRentalService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<CarRental> GetCarRentals()
    {
        return context.CarRentals
            .Include(Cr => Cr.Business)
            .Include(Cr => Cr.CarRentalBookings)
            .ToList();
    }
}