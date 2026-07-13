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

    public Restaurant? GetRestaurantFullDetails(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public List<Restaurant> GetRestaurants()
    {
        return context.Restaurants
            .Where(restaurant => !restaurant.IsDeleted)
            .Include(restaurant => restaurant.Business)
            .Include(restaurant => restaurant.Images)
            .Include(restaurant => restaurant.Reviews)
            .Include(restuarant => restuarant.Tables)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Restaurant> GetRestaurantsByBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public Restaurant? GetRestaurantWithTables(int restaurantId)
    {
        throw new NotImplementedException();
    }
}
