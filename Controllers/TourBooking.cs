using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class TourBookingController : Controller
{
    ITourBooking tourBookingService;

    public TourBookingController(ITourBooking tourBookingService)
    {
        this.tourBookingService = tourBookingService;
    }

    public IActionResult Index()
    {
        return View(tourBookingService.GetAll());
    }
}