using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITour : IService<Tour>
{
    List<Tour> GetTours();
}
