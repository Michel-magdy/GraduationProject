using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotelReview : IService<HotelReview>
{
    List<HotelReview> GetReviews();
}
