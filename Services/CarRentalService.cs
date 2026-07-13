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

    public IEnumerable<CarRental> GetAvailableCars()
    {
        throw new NotImplementedException();
    }

    public List<CarRental> GetCarRentals()
    {
        return context.CarRentals
            .Where(carRental => !carRental.IsDeleted)
            .Include(Cr => Cr.Business)
            .Include(Cr => Cr.Images)
            .Include(Cr => Cr.Reviews)
            .Include(Cr => Cr.CarRentalBookings)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<CarRental> GetCarsByBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public bool IsCarAvailable(int carId)
    {
        throw new NotImplementedException();
    }
}
