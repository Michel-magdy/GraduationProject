using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITour : IService<Tour>
{
    IEnumerable<Tour> GetToursByBusiness(int businessId);

    IEnumerable<Tour> GetUpcomingTours();

    int GetRemainingSeats(int tourId);
}
