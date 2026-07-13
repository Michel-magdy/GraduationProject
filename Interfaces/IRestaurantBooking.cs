using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRestaurantBooking : IService<RestaurantBooking>
{
    IEnumerable<RestaurantBooking> GetBookingsByRestaurant(int restaurantId);

    IEnumerable<RestaurantBooking> GetBookingsByUser(int userId);

    void ConfirmBooking(int bookingId);

    void CancelBooking(int bookingId);
}
