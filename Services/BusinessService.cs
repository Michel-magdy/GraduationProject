using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class BusinessService : GenericService<Business>, IBusiness
{
    readonly private Context context;
    public BusinessService(Context _context) : base(_context)
    {
        context = _context;
    }

}