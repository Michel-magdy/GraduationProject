using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class CarRentalReviewService : GenericService<CarRentalReview>, ICarRentalReview
{
    readonly private Context context;
    public CarRentalReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

    public double GetAverageRating(int carId)
    {
        var reviews = context.CarRentalReviews.Where(r => r.CarRentalId == carId).ToList();
        return reviews.Any() ? reviews.Average(r => r.Rate) : 0;
    }

    public IEnumerable<CarRentalReview> GetCarReviews(int carId)
    {
        return context.CarRentalReviews
            .Include(r => r.User)
            .Where(r => r.CarRentalId == carId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    public List<CarRentalReview> GetReviews()
    {
        return context.CarRentalReviews
            .Include(review => review.User)
            .Include(review => review.CarRental)
            .ToList();
    }
}
