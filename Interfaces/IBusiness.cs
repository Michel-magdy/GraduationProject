using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IBusiness : IService<Business>
{
    IEnumerable<Business> GetPendingBusinesses();

    IEnumerable<Business> GetApprovedBusinesses();

    IEnumerable<Business> GetRejectedBusinesses();

    IEnumerable<Business> GetBusinessesByOwner(int ownerId);

    Business? GetBusinessDetails(int businessId);

    void ApproveBusiness(int businessId);

    void RejectBusiness(int businessId);

    bool BusinessExists(string businessName);
}
