using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RestaurantReviewService : GenericService<RestaurantReview>, IRestaurantReview
{
    readonly private Context context;
    public RestaurantReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

    public double GetAverageRating(int restaurantId)
    {
        var reviews = context.RestaurantReviews.Where(r => r.RestaurantId == restaurantId).ToList();
        return reviews.Any() ? reviews.Average(r => r.Rate) : 0;
    }

    public IEnumerable<RestaurantReview> GetRestaurantReviews(int restaurantId)
    {
        return context.RestaurantReviews
            .Include(r => r.User)
            .Where(r => r.RestaurantId == restaurantId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    public List<RestaurantReview> GetReviews()
    {
        return context.RestaurantReviews
            .Include(review => review.User)
            .Include(review => review.Restaurant)
            .ToList();
    }
}
