using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class HotelReviewService : GenericService<HotelReview>, IHotelReview
{
    readonly private Context context;
    public HotelReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<HotelReview> GetReviews()
    {
        return context.HotelReviews
            .Include(review => review.User)
            .Include(review => review.Hotel)
            .ToList();
    }
}
