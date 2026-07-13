using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class RestaurantReviewService : GenericService<RestaurantReview>, IRestaurantReview
{
    readonly private Context context;
    public RestaurantReviewService(Context _context) : base(_context)
    {
        context = _context;
    }


    List<RestaurantReview> IRestaurantReview.GetReviews()
    {
        throw new NotImplementedException();
    }
}