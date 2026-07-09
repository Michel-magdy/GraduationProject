using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class HotelService : GenericService<Hotel>, IHotel
{
    readonly private Context context;
    public HotelService(Context _context) : base(_context)
    {
        context = _context;
    }

}