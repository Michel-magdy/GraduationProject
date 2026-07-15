using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITour : IService<Tour>
{
    List<Tour> GetTours();

    IEnumerable<Tour> GetToursByBusiness(int businessId);

    IEnumerable<Tour> GetUpcomingTours();

    int GetRemainingSeats(int tourId);

    Tour? GetTourFullDetails(int tourId);

    void UpdateTour(Tour tour);

    void AddImage(int tourId, string imagePath);

    void UpdateImage(int tourId, int imageId, string imagePath);

    void DeleteImage(int tourId, int imageId);
}