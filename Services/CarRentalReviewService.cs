using GraduationProject.Interfaces;
using GraduationProject.Models;

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
        throw new NotImplementedException();
    }
}