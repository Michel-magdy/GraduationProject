using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurant : IService<Restaurant>
{
    IEnumerable<Restaurant> GetRestaurantsByBusiness(int businessId);

    Restaurant? GetRestaurantWithTables(int restaurantId);
    public List<Restaurant> GetRestaurants();

    Restaurant? GetRestaurantFullDetails(int restaurantId);
    void UpdateRestaurant(Restaurant restaurant);

    void AddImage(int restaurantId, string imagePath);

    void UpdateImage(int restaurantId, int imageId, string imagePath);

    void DeleteImage(int restaurantId, int imageId);
}
