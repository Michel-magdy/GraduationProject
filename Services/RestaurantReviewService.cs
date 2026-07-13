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


    public List<RestaurantReview> GetReviews()
    {
        return context.RestaurantReviews
            .Include(review => review.User)
            .Include(review => review.Restaurant)
            .ToList();
    }
}
