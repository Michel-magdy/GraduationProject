using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class TourService : GenericService<Tour>, ITour
{
    readonly private Context context;
    public TourService(Context _context) : base(_context)
    {
        context = _context;
    }

    public int GetRemainingSeats(int tourId)
    {
        throw new NotImplementedException();
    }

    public List<Tour> GetTours()
    {
        return context.Tours
            .Where(tour => !tour.IsDeleted)
            .Include(T => T.Business)
            .Include(T => T.Images)
            .Include(T => T.Reviews)
            .Include(T => T.TourBookings)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Tour> GetToursByBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Tour> GetUpcomingTours()
    {
        throw new NotImplementedException();
    }
}
