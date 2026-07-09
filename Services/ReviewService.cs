using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class ReviewService : GenericService<Review>, IReview
{
    readonly private Context context;
    public ReviewService(Context _context) : base(_context)
    {
        context = _context;
    }

}