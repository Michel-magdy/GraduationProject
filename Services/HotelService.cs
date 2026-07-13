using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class HotelService : GenericService<Hotel>, IHotel
{
    readonly private Context context;
    public HotelService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Hotel> GetHotels()
    {
        return context.Hotels
            .Where(hotel => !hotel.IsDeleted)
            .Include(hotel => hotel.Business)
            .Include(hotel => hotel.Images)
            .Include(hotel => hotel.Reviews)
            .Include(hotel => hotel.Rooms)
            .AsSplitQuery()
            .ToList();
    }
}
