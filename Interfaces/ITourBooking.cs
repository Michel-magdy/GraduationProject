using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITourBooking : IService<TourBooking>
{
    IEnumerable<TourBooking> GetBookings();

    TourBooking? GetBookingWithDetails(int id);

    IEnumerable<TourBooking> GetBookingsByTour(int tourId);

    IEnumerable<TourBooking> GetBookingsByUser(int userId);

    void CancelBooking(int bookingId);
}