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

    public List<CarRentalReview> GetReviews()
    {
        return context.CarRentalReviews
            .Include(review => review.User)
            .Include(review => review.CarRental)
            .ToList();
    }
}
