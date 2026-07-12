using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRentalBooking : IService<CarRentalBooking>
{
    List<CarRentalBooking> GetCarRentalBookings();
}
