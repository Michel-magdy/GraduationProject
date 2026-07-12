using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class RestaurantBookingController : Controller
{
    IRestaurantBooking restaurantBookingService;

    public RestaurantBookingController(IRestaurantBooking restaurantBookingService)
    {
        this.restaurantBookingService = restaurantBookingService;
    }

    public IActionResult Index()
    {
        return View(restaurantBookingService.GetRestaurantBookings());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        restaurantBookingService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
