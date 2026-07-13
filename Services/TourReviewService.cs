using GraduationProject.Interfaces;
using GraduationProject.Models;

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
        throw new NotImplementedException();
    }
}