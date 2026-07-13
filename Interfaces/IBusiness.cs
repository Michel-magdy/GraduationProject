using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IBusiness : IService<Business>
{
    IEnumerable<Business> GetPendingBusinesses();

    IEnumerable<Business> GetApprovedBusinesses();

    IEnumerable<Business> GetRejectedBusinesses();
    List<Business> GetBusinessesData();

    IEnumerable<Business> GetBusinessesByOwner(int ownerId);

    Business? GetBusinessDetails(int businessId);

    void UpdateBusiness(Business business);

    void AddImage(int businessId, string imagePath);

    void UpdateImage(int businessId, int imageId, string imagePath);

    void DeleteImage(int businessId, int imageId);

    void ApproveBusiness(int businessId);

    void RejectBusiness(int businessId);

    bool BusinessExists(string businessName);
}
