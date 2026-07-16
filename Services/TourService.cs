using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class TourService : GenericService<Tour>, ITour
{
    readonly private Context context;
    public TourService(Context _context) : base(_context)
    {
        context = _context;
    }

    public int GetRemainingSeats(int tourId)
    {
        var tour = context.Tours.FirstOrDefault(t => t.Id == tourId && !t.IsDeleted);

        return tour?.RemainingSeats ?? 0;
    }

    public List<Tour> GetTours()
    {
        return context.Tours
            .Where(tour => !tour.IsDeleted)
            .Include(T => T.Business)
            .Include(T => T.Images)
            .Include(T => T.Reviews)
            .Include(T => T.TourBookings)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Tour> GetToursByBusiness(int businessId)
    {
        return context.Tours
            .Where(tour => !tour.IsDeleted && tour.BusinessId == businessId)
            .Include(T => T.Business)
            .Include(T => T.Images)
            .Include(T => T.Reviews)
            .Include(T => T.TourBookings)
            .AsSplitQuery()
            .ToList();
    }

    public IEnumerable<Tour> GetUpcomingTours()
    {
        var today = DateTime.Today;

        return context.Tours
            .Where(tour => !tour.IsDeleted && tour.TripDate >= today)
            .Include(T => T.Business)
            .Include(T => T.Images)
            .Include(T => T.Reviews)
            .Include(T => T.TourBookings)
            .AsSplitQuery()
            .ToList();
    }

    public Tour? GetTourFullDetails(int tourId)
    {
        return context.Tours
            .Where(t => !t.IsDeleted)
            .Include(t => t.Business)
            .Include(t => t.Images)
            .Include(t => t.Reviews)
                .ThenInclude(t => t.User)
            .Include(t => t.TourBookings)
                .ThenInclude(t => t.User)
            .FirstOrDefault(t => t.Id == tourId);
    }

    public void UpdateTour(Tour tour)
    {
        var existingTour = context.Tours
            .Include(t => t.Images)
            .FirstOrDefault(t => t.Id == tour.Id);

        if (existingTour == null)
        {
            return;
        }

        existingTour.Name = tour.Name;
        existingTour.TripDate = tour.TripDate;
        existingTour.Price = tour.Price;
        existingTour.MaxParticipants = tour.MaxParticipants;
        existingTour.CurrentParticipants = tour.CurrentParticipants;

        // Filter out submitted images with empty paths
        var submittedImages = tour.Images
            .Where(i => !string.IsNullOrWhiteSpace(i.ImagePath))
            .ToList();

        // Delete images that were removed in the form
        var submittedImageIds = submittedImages
            .Where(i => i.Id != 0)
            .Select(i => i.Id)
            .ToHashSet();

        var imagesToRemove = existingTour.Images
            .Where(i => !submittedImageIds.Contains(i.Id))
            .ToList();

        foreach (var imageToRemove in imagesToRemove)
        {
            context.Images.Remove(imageToRemove);
        }

        // Update existing images
        foreach (var existingImage in existingTour.Images)
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
            existingTour.Images.Add(new Image
            {
                TourId = existingTour.Id,
                ImagePath = newImage.ImagePath.Trim()
            });
        }

        context.SaveChanges();
    }

    public void AddImage(int tourId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        context.Images.Add(new Image
        {
            TourId = tourId,
            ImagePath = imagePath.Trim()
        });
        context.SaveChanges();
    }

    public void UpdateImage(int tourId, int imageId, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.TourId == tourId);

        if (image == null)
        {
            return;
        }

        image.ImagePath = imagePath.Trim();
        context.SaveChanges();
    }

    public void DeleteImage(int tourId, int imageId)
    {
        var image = context.Images.FirstOrDefault(image => image.Id == imageId && image.TourId == tourId);

        if (image == null)
        {
            return;
        }

        context.Images.Remove(image);
        context.SaveChanges();
    }
}