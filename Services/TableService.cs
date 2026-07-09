using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class TableService : GenericService<Table>, ITable
{
    readonly private Context context;
    public TableService(Context _context) : base(_context)
    {
        context = _context;
    }

}