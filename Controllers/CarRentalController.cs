using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class CarRentalController : Controller
{
    ICarRental carRentalService;

    public CarRentalController(ICarRental carRentalService)
    {
        this.carRentalService = carRentalService;
    }

    public IActionResult Index()
    {
        return View(carRentalService.GetCarRentals());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        carRentalService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
