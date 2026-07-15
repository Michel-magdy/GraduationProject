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
        return context.Tables
            .Where(t => t.RestaurantId == restaurantId && t.Status == TableStatus.Available)
            .Include(t => t.Restaurant)
            .ToList();
    }

    public List<Table> GetTables()
    {
        return context.Tables
            .Include(t => t.Restaurant)
            .Include(t => t.RestaurantBookings)
            .ToList();
    }

    public IEnumerable<Table> GetTablesByRestaurant(int restaurantId)
    {
        return context.Tables
            .Where(t => t.RestaurantId == restaurantId)
            .Include(t => t.Restaurant)
            .Include(t => t.RestaurantBookings)
            .ToList();
    }

    public bool IsTableAvailable(int tableId)
    {
        var table = context.Tables.Find(tableId);
        return table != null && table.Status == TableStatus.Available;
    }

    public Table? GetTableWithDetails(int tableId)
    {
        return context.Tables
            .Include(t => t.Restaurant)
            .Include(t => t.RestaurantBookings)
                .ThenInclude(rb => rb.User)
            .FirstOrDefault(t => t.Id == tableId);
    }
}