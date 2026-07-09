using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class RoleService : GenericService<Role>, IRole
{
    readonly private Context context;
    public RoleService(Context _context) : base(_context)
    {
        context = _context;
    }

}