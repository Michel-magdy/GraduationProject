using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurant : IService<Restaurant>
{
    IEnumerable<Restaurant> GetRestaurantsByBusiness(int businessId);

    Restaurant? GetRestaurantWithTables(int restaurantId);

    Restaurant? GetRestaurantFullDetails(int restaurantId);
}
