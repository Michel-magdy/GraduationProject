using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IBusiness : IService<Business>
{
    List<Business> GetBusinessWithOwner();
    List<Business> GetBusinessesForIndex();
}
