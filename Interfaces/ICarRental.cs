using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ICarRental : IService<CarRental>
{
    List<CarRental> GetCarRentals();
}
