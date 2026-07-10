using System.Net.Http.Headers;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RestaurantService : GenericService<Restaurant>, IRestaurant
{
    readonly private Context context;
    public RestaurantService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Restaurant> GetWithBusinessAndTable()
    {
        return context.Restaurants
            .Include(restaurant => restaurant.Business)
            .Include(restuarant => restuarant.Tables)
            .ToList();
    }
}