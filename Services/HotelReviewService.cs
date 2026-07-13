using GraduationProject.Interfaces;
using GraduationProject.Models;

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
        throw new NotImplementedException();
    }
}