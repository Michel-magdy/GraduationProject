using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IBusiness : IService<Business>
{
    List<Business> GetBusinessWithOwner();
    List<Business> GetBusinessesForIndex();
    Business? GetBusinessDetails(int id);
    void UpdateBusiness(Business business);
    void AddImage(int businessId, string imagePath);
    void UpdateImage(int businessId, int imageId, string imagePath);
    void DeleteImage(int businessId, int imageId);
}
