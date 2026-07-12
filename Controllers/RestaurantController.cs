using GraduationProject.Interfaces;
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
}