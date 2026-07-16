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

    public double GetAverageRating(int hotelId)
    {
        var reviews = context.HotelReviews.Where(r => r.HotelId == hotelId).ToList();
        return reviews.Any() ? reviews.Average(r => r.Rate) : 0;
    }

    public IEnumerable<HotelReview> GetHotelReviews(int hotelId)
    {
        return context.HotelReviews
            .Include(r => r.User)
            .Where(r => r.HotelId == hotelId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    public List<HotelReview> GetReviews()
    {
        return context.HotelReviews
            .Include(review => review.User)
            .Include(review => review.Hotel)
            .ToList();
    }
}
