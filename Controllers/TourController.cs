using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class TourController : Controller
{
    ITour tourService;

    public TourController(ITour tourService)
    {
        this.tourService = tourService;
    }

    public IActionResult Index()
    {
        return View(tourService.GetAll());
    }
}