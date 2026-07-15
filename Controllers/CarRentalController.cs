using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class CarRentalController : Controller
{
    ICarRental CarRentalService;
    IBusiness BusinessService;

    public CarRentalController(ICarRental carRentalService, IBusiness businessService)
    {
        CarRentalService = carRentalService;
        BusinessService = businessService;
    }

    public IActionResult Index(int? businessId)
    {
        if (businessId.HasValue)
        {
            var business = BusinessService.GetById(businessId.Value);

            if (business == null)
                return NotFound();

            ViewBag.BusinessId = business.Id;
            ViewBag.BusinessName = business.BusinessName;

            var businessCarRentals = CarRentalService.GetCarsByBusiness(business.Id).ToList();
            return View(businessCarRentals);
        }

        ViewBag.BusinessName = "All";
        var carRentals = CarRentalService.GetCarRentals();
        return View(carRentals);
    }

    public IActionResult Details(int id)
    {
        var carRental = CarRentalService.GetCarRentalFullDetails(id);
        if (carRental == null)
            return NotFound();

        return View(carRental);
    }

    public IActionResult Create(int businessId)
    {
        var business = BusinessService.GetById(businessId);

        if (business == null)
            return NotFound();

        ViewBag.BusinessId = businessId;
        ViewBag.BusinessName = business.BusinessName;

        return View(new CarRental { BusinessId = businessId });
    }

    public IActionResult Edit(int id)
    {
        var carRental = CarRentalService.GetCarRentalFullDetails(id);
        if (carRental == null)
            return NotFound();

        return View(carRental);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CarRental carRental)
    {
        if (!ModelState.IsValid)
        {
            return View(carRental);
        }
        CarRentalService.UpdateCarRental(carRental);
        return RedirectToAction(nameof(Details), new { id = carRental.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CarRental carRental)
    {
        if (!ModelState.IsValid)
        {
            var business = BusinessService.GetById(carRental.BusinessId);
            ViewBag.BusinessId = carRental.BusinessId;
            ViewBag.BusinessName = business?.BusinessName;
            return View(carRental);
        }

        CarRentalService.Add(carRental);
        return RedirectToAction(nameof(Index), new { businessId = carRental.BusinessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var carRental = CarRentalService.GetById(id);

        if (carRental == null)
            return NotFound();

        CarRentalService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddImage(int carRentalId, string imagePath, string returnAction = nameof(Details))
    {
        CarRentalService.AddImage(carRentalId, imagePath);
        return RedirectToAction(returnAction, new { id = carRentalId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateImage(int carRentalId, int imageId, string imagePath)
    {
        CarRentalService.UpdateImage(carRentalId, imageId, imagePath);
        return RedirectToAction(nameof(Edit), new { id = carRentalId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteImage(int carRentalId, int imageId, string returnAction = nameof(Details))
    {
        CarRentalService.DeleteImage(carRentalId, imageId);
        return RedirectToAction(returnAction, new { id = carRentalId });
    }
}