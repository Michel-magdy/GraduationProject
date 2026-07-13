using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITable : IService<Table>
{
    IEnumerable<Table> GetTablesByRestaurant(int restaurantId);

    IEnumerable<Table> GetAvailableTables(int restaurantId);
}
