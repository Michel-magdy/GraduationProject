using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITourReview : IService<TourReview>
{
    List<TourReview> GetReviews();
}
