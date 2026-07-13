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
        throw new NotImplementedException();
    }

    public IEnumerable<RestaurantReview> GetRestaurantReviews(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public List<RestaurantReview> GetReviews()
    {
        return context.RestaurantReviews
            .Include(review => review.User)
            .Include(review => review.Restaurant)
            .ToList();
    }
}
