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

    public double GetAverageRating(int tourId)
    {
        var reviews = context.TourReviews.Where(r => r.TourId == tourId).ToList();
        return reviews.Any() ? reviews.Average(r => r.Rate) : 0;
    }

    public List<TourReview> GetReviews()
    {
        return context.TourReviews
            .Include(review => review.User)
            .Include(review => review.Tour)
            .ToList();
    }

    public IEnumerable<TourReview> GetTourReviews(int tourId)
    {
        return context.TourReviews
            .Include(r => r.User)
            .Where(r => r.TourId == tourId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }
}
