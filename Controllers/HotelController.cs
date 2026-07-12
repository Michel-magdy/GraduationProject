using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class HotelController : Controller
{
    IHotel hotelservice;

    public HotelController(IHotel hotelservice)
    {
        this.hotelservice = hotelservice;
    }

    public IActionResult Index()
    {
        return View(hotelservice.GetHotels());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        hotelservice.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
