using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class RestaurantController : Controller
{
    IRestaurant RestaurantService;

    public RestaurantController(IRestaurant restaurantService)
    {
        RestaurantService = restaurantService;
    }

    public IActionResult Index()
    {
        return View(RestaurantService.GetRestaurants());
    }

    public IActionResult Add()
    {
        return View(new Restaurant());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        RestaurantService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
