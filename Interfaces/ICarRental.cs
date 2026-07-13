using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRental : IService<CarRental>
{
    IEnumerable<CarRental> GetCarsByBusiness(int businessId);

    IEnumerable<CarRental> GetAvailableCars();

    bool IsCarAvailable(int carId);
}
