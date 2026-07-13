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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
