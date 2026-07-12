using System.Security;
using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class HotelBookingController : Controller
{
    IHotelBooking hotelBookingService;

    public HotelBookingController(IHotelBooking hotelBookingService)
    {
        this.hotelBookingService = hotelBookingService;
    }


    public IActionResult Index()
    {
        return View(hotelBookingService.GetHotelBooking());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        hotelBookingService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
