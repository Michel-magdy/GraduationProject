using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRental : IService<CarRental>
{
    List<CarRental> GetCarRentals();

    IEnumerable<CarRental> GetCarsByBusiness(int businessId);

    IEnumerable<CarRental> GetAvailableCars();

    bool IsCarAvailable(int carId);

    CarRental? GetCarRentalFullDetails(int carRentalId);

    void UpdateCarRental(CarRental carRental);

    void AddImage(int carRentalId, string imagePath);

    void UpdateImage(int carRentalId, int imageId, string imagePath);

    void DeleteImage(int carRentalId, int imageId);
}