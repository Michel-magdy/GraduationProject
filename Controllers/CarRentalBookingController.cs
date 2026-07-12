using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class CarRentalBookingController : Controller
{
    ICarRentalBooking carRentalBookingService;

    public CarRentalBookingController(ICarRentalBooking carRentalBookingService)
    {
        this.carRentalBookingService = carRentalBookingService;
    }

    public IActionResult Index()
    {
        return View(carRentalBookingService.GetCarRentalBookings());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        carRentalBookingService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
