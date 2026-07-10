using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class BusinessService : GenericService<Business>, IBusiness
{
    readonly private Context context;
    public BusinessService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Business> GetBusinessWithOwner()
    {
        return context.Businesses.Include(business => business.Owner).ToList();
    }

    public List<Business> GetBusinessesForIndex()
    {
        return context.Businesses
            .Include(business => business.Owner)
            .Include(business => business.Hotels)
            .Include(business => business.Restaurants)
            .Include(business => business.CarRentals)
            .Include(business => business.Tours)
            .Include(business => business.Images)
            .Include(business => business.Reviews)
            .AsSplitQuery()
            .ToList();
    }
}
