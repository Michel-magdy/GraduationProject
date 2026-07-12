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

    public List<Table> GetTables()
    {
        return context.Tables
            .Include(T => T.Restaurant)
            .Include(T => T.RestaurantBookings)
            .ToList();
    }
}