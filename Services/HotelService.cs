using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class HotelService : GenericService<Hotel>, IHotel
{
    readonly private Context context;
    public HotelService(Context _context) : base(_context)
    {
        context = _context;
    }

    public Hotel? GetHotelFullDetails(int hotelId)
    {
        return context.Hotels
            .Where(h => !h.IsDeleted)
            .Include(h => h.Business)
            .Include(h => h.Rooms)
            .Include(h => h.Images)
            .Include(h => h.Reviews)
            .FirstOrDefault(h => h.Id == hotelId);
    }

    public void UpdateHotel(Hotel hotel)
    {
        var existingHotel = context.Hotels
            .Include(h => h.Images)
            .FirstOrDefault(h => h.Id == hotel.Id);

        if (existingHotel == null)
        {
            return;
        }

        existingHotel.Name = hotel.Name;
        existingHotel.Location = hotel.Location;
        existingHotel.Description = hotel.Description;

        // Filter out submitted images with empty paths
        var submittedImages = hotel.Images
            .Where(i => !string.IsNullOrWhiteSpace(i.ImagePath))
            .ToList();

        // Delete images that were removed in the form
        var submittedImageIds = submittedImages
            .Where(i => i.Id != 0)
            .Select(i => i.Id)
            .ToHashSet();

        var imagesToRemove = existingHotel.Images
            .Where(i => !submittedImageIds.Contains(i.Id))
            .ToList();

        foreach (var imageToRemove in imagesToRemove)
        {
            context.Images.Remove(imageToRemove);
        }

        // Update existing images
        foreach (var existingImage in existingHotel.Images)
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
            existingHotel.Images.Add(new Image
            {
                HotelId = existingHotel.Id,
                ImagePath = newImage.ImagePath.Trim()
            });
        }

        context.SaveChanges();
    }

    public List<Hotel> GetHotels()
    {
        return context.Hotels
            .Where(hotel => !hotel.IsDeleted)
            .Include(hotel => hotel.Business)
            .Include(hotel => hotel.Images)
            .Include(hotel => hotel.Reviews)
            .Include(hotel => hotel.Rooms)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Hotel> GetHotelsByBusiness(int businessId)
    {
        throw new NotImplementedException();
    }

    public Hotel? GetHotelWithRooms(int hotelId)
    {
        throw new NotImplementedException();
    }

    public void AddImage(int hotelId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        context.Images.Add(new Image
        {
            HotelId = hotelId,
            ImagePath = imagePath.Trim()
        });
        context.SaveChanges();
    }

    public void UpdateImage(int hotelId, int imageId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.HotelId == hotelId);

        if (image == null)
        {
            return;
        }

        image.ImagePath = imagePath.Trim();
        context.SaveChanges();
    }

    public void DeleteImage(int hotelId, int imageId)
    {
        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.HotelId == hotelId);

        if (image == null)
        {
            return;
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

}
