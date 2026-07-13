using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class BusinessService : GenericService<Business>, IBusiness
{
    private readonly Context context;

    public BusinessService(Context _context) : base(_context)
    {
        context = _context;
    }

    public List<Business> GetBusinessWithOwner()
    {
        return context.Businesses
            .Where(business => !business.IsDeleted)
            .Include(business => business.Owner)
            .ToList();
    }


    public Business? GetBusinessDetails(int id)
    {
        return context.Businesses
               .Where(b => !b.IsDeleted)
               .Include(b => b.Owner)
               .Include(b => b.Images)
               .Include(b => b.Hotels)
               .Include(b => b.Restaurants)
               .Include(b => b.Tours)
               .Include(b => b.CarRentals)
               .FirstOrDefault(b => b.Id == id);
    }

    public void UpdateBusiness(Business business)
    {
        var existingBusiness = context.Businesses.Find(business.Id);

        if (existingBusiness == null)
        {
            return;
        }

        existingBusiness.BusinessName = business.BusinessName;
        existingBusiness.Address = business.Address;
        existingBusiness.Status = business.Status;
        existingBusiness.Description = business.Description;

        context.SaveChanges();
    }

    public void AddImage(int businessId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        context.Images.Add(new Image
        {
            BusinessId = businessId,
            ImagePath = imagePath.Trim()
        });
        context.SaveChanges();
    }

    public void UpdateImage(int businessId, int imageId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.BusinessId == businessId);

        if (image == null)
        {
            return;
        }

        image.ImagePath = imagePath.Trim();
        context.SaveChanges();
    }

    public void DeleteImage(int businessId, int imageId)
    {
        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.BusinessId == businessId);

        if (image == null)
        {
            return;
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

    public IEnumerable<Business> GetPendingBusinesses()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Business> GetApprovedBusinesses()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Business> GetRejectedBusinesses()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Business> GetBusinessesByOwner(int ownerId)
    {
        throw new NotImplementedException();
    }

    public void ApproveBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public void RejectBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public bool BusinessExists(string businessName)
    {
        throw new NotImplementedException();
    }

    public List<Business> GetBusinessData()
    {
        return context.Businesses
               .Where(b => !b.IsDeleted)
               .Include(b => b.Owner)
               .Include(b => b.Images)
               .Include(b => b.Hotels)
               .Include(b => b.Restaurants)
               .Include(b => b.Tours)
               .Include(b => b.CarRentals)
               .ToList();
    }
}
