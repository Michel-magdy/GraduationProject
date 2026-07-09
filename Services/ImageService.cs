using GraduationProject.Interfaces;
using GraduationProject.Models;

namespace GraduationProject.Services;

public class ImageService : GenericService<Image>, IImage
{
    readonly private Context context;
    public ImageService(Context _context) : base(_context)
    {
        context = _context;
    }

}