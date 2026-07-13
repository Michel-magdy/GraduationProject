using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class TourReviewService : GenericService<TourReview>, ITourReview
{
    readonly private Context context;
    public TourReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<TourReview> GetReviews()
    {
        return context.TourReviews
            .Include(review => review.User)
            .Include(review => review.Tour)
            .ToList();
    }
}
