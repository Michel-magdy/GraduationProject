using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IHotelBooking : IService<HotelBooking>
{
    IEnumerable<HotelBooking> GetBookingsByUser(int userId);

    IEnumerable<HotelBooking> GetBookingsByHotel(int hotelId);

    void ConfirmBooking(int bookingId);

    void CancelBooking(int bookingId);

    decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut);
}
