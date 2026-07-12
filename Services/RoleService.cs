using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RoleService : GenericService<Role>, IRole
{
    readonly private Context context;
    public RoleService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Role> GetRoles()
    {
        return context.Roles.Include(role => role.Users).ToList();
    }
}