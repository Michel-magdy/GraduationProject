using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRentalReview : IService<CarRentalReview>
{
    List<CarRentalReview> GetReviews();
}
