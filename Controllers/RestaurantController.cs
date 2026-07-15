using GraduationProject.Interfaces;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class RestaurantController : Controller
{

    IRestaurant RestaurantService;
    ITable TableService;
    IBusiness BusinessService;

    public RestaurantController(IRestaurant restaurantService, ITable tableService, IBusiness businessService)
    {
        RestaurantService = restaurantService;
        TableService = tableService;
        BusinessService = businessService;
    }

    public IActionResult Index(int? businessId)
    {
        if (businessId.HasValue)
        {
            var Business = BusinessService.GetById(businessId.Value);

            if (Business == null)
                return NotFound();

            ViewBag.BusinessId = Business.Id;
            ViewBag.BusinessName = Business.BusinessName;

            var businessHotels = RestaurantService.GetRestaurantsByBusiness(Business.Id).ToList();
            return View(businessHotels);

        }

        ViewBag.BusinessName = "All";
        return View(RestaurantService.GetRestaurants());
    }

    public IActionResult Details(int id)
    {
        var Restaurant = RestaurantService.GetRestaurantFullDetails(id);
        if (Restaurant == null)
            return NotFound();

        return View(Restaurant);
    }
    public IActionResult Create(int businessId)
    {
        var business = BusinessService.GetById(businessId);

        if (business == null)
            return NotFound();

        ViewBag.BusinessId = businessId;
        ViewBag.BusinessName = business.BusinessName;

        return View(new Restaurant { BusinessId = businessId });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Restaurant restaurant, List<string> ImageUrls)
    {
        if (!ModelState.IsValid)
        {
            var business = BusinessService.GetById(restaurant.BusinessId);
            ViewBag.BusinessId = restaurant.BusinessId;
            ViewBag.BusinessName = business?.BusinessName;
            return View(restaurant);
        }

        RestaurantService.Add(restaurant);

        if (ImageUrls != null)
        {
            foreach (var url in ImageUrls)
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    RestaurantService.AddImage(restaurant.Id, url);
                }
            }
        }

        return RedirectToAction(nameof(Index), new { businessId = restaurant.BusinessId });
    }

    public IActionResult Edit(int id)
    {
        var restaurant = RestaurantService.GetRestaurantFullDetails(id);
        if (restaurant == null)
            return NotFound();

        ViewBag.Tables = new SelectList(TableService.GetAll(), "Id", "TableNumber");
        return View(restaurant);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Restaurant restaurant)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Tables = new SelectList(TableService.GetAll(), "Id", "TableNumber");
            return View(restaurant);
        }
        RestaurantService.UpdateRestaurant(restaurant);
        return RedirectToAction(nameof(Details), new { id = restaurant.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var restaurant = RestaurantService.GetById(id);

        if (restaurant == null)
            return NotFound();

        RestaurantService.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddImage(int retstaurantId, string imagePath, string returnAction = nameof(Details))
    {
        RestaurantService.AddImage(retstaurantId, imagePath);
        return RedirectToAction(returnAction, new { id = retstaurantId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateImage(int retstaurantId, int imageId, string imagePath)
    {
        RestaurantService.UpdateImage(retstaurantId, imageId, imagePath);
        return RedirectToAction(nameof(Edit), new { id = retstaurantId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteImage(int retstaurantId, int imageId, string returnAction = nameof(Details))
    {
        RestaurantService.DeleteImage(retstaurantId, imageId);
        return RedirectToAction(returnAction, new { id = retstaurantId });
    }

}
