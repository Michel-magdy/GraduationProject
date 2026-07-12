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
        return context.Businesses.Include(business => business.Owner).ToList();
    }

    public List<Business> GetBusinessesForIndex()
    {
        return context.Businesses
            .Include(business => business.Owner)
            .Include(business => business.Hotels)
            .Include(business => business.Restaurants)
            .Include(business => business.CarRentals)
            .Include(business => business.Tours)
            .Include(business => business.Images)
            .Include(business => business.Reviews)
            .AsSplitQuery()
            .ToList();
    }

    public Business? GetBusinessDetails(int id)
    {
        return context.Businesses
            .Include(business => business.Owner)
            .Include(business => business.Images)
            .Include(business => business.Reviews)
            .AsSplitQuery()
            .FirstOrDefault(business => business.Id == id);
    }

    public void UpdateBusiness(Business business)
    {
        var existingBusiness = context.Businesses.Find(business.Id);

        if (existingBusiness == null)
        {
            return;
        }

        existingBusiness.BusinessName = business.BusinessName;
        existingBusiness.BusinessType = business.BusinessType;
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
}
