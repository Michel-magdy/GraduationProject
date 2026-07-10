using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurant : IService<Restaurant>
{
    List<Restaurant> GetWithBusinessAndTable();
}
