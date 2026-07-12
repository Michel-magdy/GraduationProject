using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class ReviewService : GenericService<Review>, IReview
{
    readonly private Context context;
    public ReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Review> GetReviews()
    {
        return context.Reviews
            .Include(review => review.User)
            .Include(review => review.Business)
            .ToList();
    }
}