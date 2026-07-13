using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurantReview : IService<RestaurantReview>
{
    IEnumerable<RestaurantReview> GetRestaurantReviews(int restaurantId);

    double GetAverageRating(int restaurantId);
}
