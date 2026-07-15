using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRentalBooking : IService<CarRentalBooking>
{
    IEnumerable<CarRentalBooking> GetBookingsByCar(int carId);

    IEnumerable<CarRentalBooking> GetBookingsByUser(int userId);

    void CancelBooking(int bookingId);
    List<CarRentalBooking> GetBookings();


    CarRentalBooking? GetBookingWithDetails(int id);
    decimal CalculateTotalPrice(int carId, DateTime start, DateTime end);
}
