using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class TableService : GenericService<Table>, ITable
{
    readonly private Context context;
    public TableService(Context _context) : base(_context)
    {
        context = _context;
    }

    public IEnumerable<Table> GetAvailableTables(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public List<Table> GetTables()
    {
        return context.Tables
            .Include(T => T.Restaurant)
            .Include(T => T.RestaurantBookings)
            .ToList();
    }

    public IEnumerable<Table> GetTablesByRestaurant(int restaurantId)
    {
        throw new NotImplementedException();
    }
}