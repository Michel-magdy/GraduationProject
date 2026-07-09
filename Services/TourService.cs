using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class TourService : GenericService<Tour>, ITour
{
    readonly private Context context;
    public TourService(Context _context) : base(_context)
    {
        context = _context;
    }

}