using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class CarRentalService : GenericService<CarRental>, ICarRental
{
    readonly private Context context;
    public CarRentalService(Context _context) : base(_context)
    {
        context = _context;
    }

    public IEnumerable<CarRental> GetAvailableCars()
    {
        return context.CarRentals
            .Where(carRental => !carRental.IsDeleted && carRental.Available)
            .Include(Cr => Cr.Business)
            .Include(Cr => Cr.Images)
            .Include(Cr => Cr.Reviews)
            .Include(Cr => Cr.CarRentalBookings)
            .AsSplitQuery()
            .ToList();
    }

    public List<CarRental> GetCarRentals()
    {
        return context.CarRentals
            .Where(carRental => !carRental.IsDeleted)
            .Include(Cr => Cr.Business)
            .Include(Cr => Cr.Images)
            .Include(Cr => Cr.Reviews)
            .Include(Cr => Cr.CarRentalBookings)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<CarRental> GetCarsByBusiness(int businessId)
    {
        return context.CarRentals
            .Where(carRental => !carRental.IsDeleted && carRental.BusinessId == businessId)
            .Include(Cr => Cr.Business)
            .Include(Cr => Cr.Images)
            .Include(Cr => Cr.Reviews)
            .Include(Cr => Cr.CarRentalBookings)
            .AsSplitQuery()
            .ToList();
    }

    public bool IsCarAvailable(int carId)
    {
        var carRental = context.CarRentals
            .FirstOrDefault(Cr => Cr.Id == carId && !Cr.IsDeleted);

        return carRental != null && carRental.Available;
    }

    public CarRental? GetCarRentalFullDetails(int carRentalId)
    {
        return context.CarRentals
            .Where(cr => !cr.IsDeleted)
            .Include(cr => cr.Business)
            .Include(cr => cr.Images)
            .Include(cr => cr.Reviews)
                .ThenInclude(cr => cr.User)
            .Include(cr => cr.CarRentalBookings)
                .ThenInclude(cr => cr.User)
            .FirstOrDefault(cr => cr.Id == carRentalId);
    }

    public void UpdateCarRental(CarRental carRental)
    {
        var existingCarRental = context.CarRentals
            .Include(cr => cr.Images)
            .FirstOrDefault(cr => cr.Id == carRental.Id);

        if (existingCarRental == null)
        {
            return;
        }

        existingCarRental.Model = carRental.Model;
        existingCarRental.Brand = carRental.Brand;
        existingCarRental.PricePerDay = carRental.PricePerDay;
        existingCarRental.Available = carRental.Available;

        // Filter out submitted images with empty paths
        var submittedImages = carRental.Images
            .Where(i => !string.IsNullOrWhiteSpace(i.ImagePath))
            .ToList();

        // Delete images that were removed in the form
        var submittedImageIds = submittedImages
            .Where(i => i.Id != 0)
            .Select(i => i.Id)
            .ToHashSet();

        var imagesToRemove = existingCarRental.Images
            .Where(i => !submittedImageIds.Contains(i.Id))
            .ToList();

        foreach (var imageToRemove in imagesToRemove)
        {
            context.Images.Remove(imageToRemove);
        }

        // Update existing images
        foreach (var existingImage in existingCarRental.Images)
        {
            var editedImage = submittedImages
                .FirstOrDefault(i => i.Id == existingImage.Id);

            if (editedImage != null)
            {
                existingImage.ImagePath = editedImage.ImagePath.Trim();
            }
        }

        // Add new images (Id == 0 means they were added in the form)
        foreach (var newImage in submittedImages.Where(i => i.Id == 0))
        {
            existingCarRental.Images.Add(new Image
            {
                CarRentalId = existingCarRental.Id,
                ImagePath = newImage.ImagePath.Trim()
            });
        }

        context.SaveChanges();
    }

    public void AddImage(int carRentalId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        context.Images.Add(new Image
        {
            CarRentalId = carRentalId,
            ImagePath = imagePath.Trim()
        });
        context.SaveChanges();
    }

    public void UpdateImage(int carRentalId, int imageId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.CarRentalId == carRentalId);

        if (image == null)
        {
            return;
        }

        image.ImagePath = imagePath.Trim();
        context.SaveChanges();
    }

    public void DeleteImage(int carRentalId, int imageId)
    {
        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.CarRentalId == carRentalId);

        if (image == null)
        {
            return;
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }
}