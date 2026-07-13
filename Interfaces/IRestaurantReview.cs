using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurantReview : IService<RestaurantReview>
{
    List<RestaurantReview> GetReviews();
}
