using System.Net.Http.Headers;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class RestaurantService : GenericService<Restaurant>, IRestaurant
{
    readonly private Context context;
    public RestaurantService(Context _context) : base(_context)
    {
        context = _context;
    }

    public void AddImage(int restaurantId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        context.Images.Add(new Image
        {
            RestaurantId = restaurantId,
            ImagePath = imagePath.Trim()
        });
        context.SaveChanges();
    }

    public void DeleteImage(int restaurantId, int imageId)
    {
        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.RestaurantId == restaurantId);

        if (image == null)
        {
            return;
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }

    public Restaurant? GetRestaurantFullDetails(int restaurantId)
    {

        return context.Restaurants
            .Where(h => !h.IsDeleted)
            .Include(h => h.Business)
            .Include(h => h.Tables)
            .Include(h => h.Images)
            .Include(h => h.Reviews)
            .FirstOrDefault(h => h.Id == restaurantId);
    }

    public List<Restaurant> GetRestaurants()
    {
        return context.Restaurants
            .Where(restaurant => !restaurant.IsDeleted)
            .Include(restaurant => restaurant.Business)
            .Include(restaurant => restaurant.Images)
            .Include(restaurant => restaurant.Reviews)
            .Include(restuarant => restuarant.Tables)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Restaurant> GetRestaurantsByBusiness(int businessId)
    {

        return context.Restaurants
            .Where(restaurant => !restaurant.IsDeleted && restaurant.BusinessId == businessId)
            .Include(restaurant => restaurant.Business)
            .Include(restaurant => restaurant.Images)
            .Include(restaurant => restaurant.Reviews)
            .Include(restaurant => restaurant.Tables)
            .AsSplitQuery()
            .ToList();
    }

    public Restaurant? GetRestaurantWithTables(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public void UpdateImage(int restaurantId, int imageId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.RestaurantId == restaurantId);

        if (image == null)
        {
            return;
        }

        image.ImagePath = imagePath.Trim();
        context.SaveChanges();
    }

    public void UpdateRestaurant(Restaurant restaurant)
    {
        var existingrestaurant = context.Restaurants
                    .Include(h => h.Images)
                    .FirstOrDefault(h => h.Id == restaurant.Id);

        if (existingrestaurant == null)
        {
            return;
        }

        existingrestaurant.Name = restaurant.Name;
        existingrestaurant.Location = restaurant.Location;
        existingrestaurant.Description = restaurant.Description;

        // Filter out submitted images with empty paths
        var submittedImages = restaurant.Images
            .Where(i => !string.IsNullOrWhiteSpace(i.ImagePath))
            .ToList();

        // Delete images that were removed in the form
        var submittedImageIds = submittedImages
            .Where(i => i.Id != 0)
            .Select(i => i.Id)
            .ToHashSet();

        var imagesToRemove = existingrestaurant.Images
            .Where(i => !submittedImageIds.Contains(i.Id))
            .ToList();

        foreach (var imageToRemove in imagesToRemove)
        {
            context.Images.Remove(imageToRemove);
        }

        // Update existing images
        foreach (var existingImage in existingrestaurant.Images)
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
            existingrestaurant.Images.Add(new Image
            {
                RestaurantId = existingrestaurant.Id,
                ImagePath = newImage.ImagePath.Trim()
            });
        }

        context.SaveChanges();
    }
}
