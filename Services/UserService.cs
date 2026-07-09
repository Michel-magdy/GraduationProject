using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class UserService : GenericService<User>, IUser
{
    readonly private Context context;
    public UserService(Context _context) : base(_context)
    {
        context = _context;
    }

}