using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITourReview : IService<TourReview>
{
    IEnumerable<TourReview> GetTourReviews(int tourId);

    double GetAverageRating(int tourId);
}
