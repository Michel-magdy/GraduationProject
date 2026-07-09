using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class CarRentalService : GenericService<CarRental>, ICarRental
{
    readonly private Context context;
    public CarRentalService(Context _context) : base(_context)
    {
        context = _context;
    }

}