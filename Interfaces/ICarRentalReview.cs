using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRentalReview : IService<CarRentalReview>
{
    IEnumerable<CarRentalReview> GetCarReviews(int carId);

    double GetAverageRating(int carId);
}
