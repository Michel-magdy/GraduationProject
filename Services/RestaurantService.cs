using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class RestaurantService : GenericService<Restaurant>, IRestaurant
{
    readonly private Context context;
    public RestaurantService(Context _context) : base(_context)
    {
        context = _context;
    }

}