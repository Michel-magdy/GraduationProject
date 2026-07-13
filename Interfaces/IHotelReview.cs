using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotelReview : IService<HotelReview>
{
    IEnumerable<HotelReview> GetHotelReviews(int hotelId);

    double GetAverageRating(int hotelId);
}
