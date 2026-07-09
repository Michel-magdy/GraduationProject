using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class RoomService : GenericService<Room>, IRoom
{
    readonly private Context context;
    public RoomService(Context _context) : base(_context)
    {
        context = _context;
    }

}